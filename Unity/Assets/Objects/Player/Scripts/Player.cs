using UnityEngine;
using System.Collections;
using InControl;

public enum Direction {
	Right,
	Left,
	Up,
	Down
}

public class Player : MonoBehaviour {
	public float runSpeed = 2;
	public float jumpHeight = 2;
	public float climbSpeed = 2;
	public float airJumpTime = 0.2f;
	public float centeringSpeed = 0.3f;
	public float footPower = 1;
	public float infection = 0;
	public float postDamageCoolDown = 0.5f;
	public GameObject defaultWeaponPrefab;

	[HideInInspector] public Transform currentClimbable;
	[HideInInspector] public bool isDead = false;

	protected float flashingLength = 0.5f;
	protected bool isFlashing = false;
	protected float flashingStartTime = 0;
	protected float lastDamageTime = 0;
	protected bool shouldJumpNextFrame = false;
	protected ControlManager controlManager;
	protected Manager manager;
	protected WeaponHolder weaponHolder;
	protected bool lastActionWasJump = false; // eventually you might add other positive y velocity things like springs, which will turn this false
	protected int originalPlatformMask;
	protected Transform currentGroundTile;
	protected Vector3 climbStartPos = Vector3.zero;
	protected float climbStartTime = 0;
	protected CharacterController2D controller;
	protected bool isClimbing = false;
	protected Vector3 outsideForce = Vector3.zero;
	protected Animator animator;
	protected float timeOfBeginFall = 0;

	void Awake() {
		controlManager = GameObject.Find("Control Manager").GetComponent<ControlManager>();
		controller = GetComponent<CharacterController2D>();
		controller.onControllerCollidedEvent += HandleControllerCollidedEvent;
		manager = GameObject.Find("Manager").GetComponent<Manager>();
		weaponHolder = GetComponentInChildren<WeaponHolder>();
		animator = GetComponent<Animator>();
	}

	void Start() {
		if (defaultWeaponPrefab != null) {
			weaponHolder.PickupWeapon(((GameObject)Instantiate(defaultWeaponPrefab)).GetComponent<Weapon>(), false);
		}
	}

	void Update() {
		if (isDead) return;

		UpdateWeapon();
		UpdateFlashing();

		Vector3 velocity = controller.velocity;
		
		ApplyDrag(ref velocity);
		UpdateRunning(ref velocity);
		UpdateClimbing(ref velocity);
		UpdateJumpingAndFalling(ref velocity);
		if (!controller.collisionState.becameGroundedThisFrame) ApplyGravity(ref velocity);
		ApplyExternalForce(ref velocity);

		controller.move(velocity * Time.deltaTime);
	}

	void UpdateWeapon() {
		weaponHolder.facingDirection = GetComponent<ObjectFlipper>().facingDirection;
		Weapon weapon = weaponHolder.currentWeapon;
		if (weapon == null) return;

		bool shouldShoot = weapon.isAutomaticFire && controlManager.GetShoot(ControlState.IsPressed) && weapon.CanShoot();
		shouldShoot = shouldShoot || (!weapon.isAutomaticFire && controlManager.GetShoot(ControlState.WasPressed));

		if (shouldShoot) weapon.Shoot();
	}

	void ApplyDrag(ref Vector3 velocity) {
		if (velocity.x > 0) velocity.x = Mathf.Max(velocity.x - manager.drag.x, 0);
		if (velocity.x < 0) velocity.x = Mathf.Min(velocity.x + manager.drag.x, 0);
		if (velocity.y > 0) velocity.y = Mathf.Max(velocity.y - manager.drag.y, 0);
		if (velocity.y < 0) velocity.y = Mathf.Min(velocity.y + manager.drag.y, 0);
	}

	void UpdateRunning(ref Vector3 velocity) {
		if (controlManager.GetRight(ControlState.IsPressed)) {
			if (controller.isGrounded) animator.SetBool("isRunning", true);

			velocity.x = Mathf.Min(velocity.x + runSpeed, runSpeed);

			GetComponent<ObjectFlipper>().Face(Direction.Right);
		}
		else if (controlManager.GetLeft(ControlState.IsPressed)) {
			if (controller.isGrounded) animator.SetBool("isRunning", true);

			velocity.x = Mathf.Max(velocity.x - runSpeed, -runSpeed);

			GetComponent<ObjectFlipper>().Face(Direction.Left);
		}
		else animator.SetBool("isRunning", false);
	}

	void UpdateClimbing(ref Vector3 velocity) {
		if (currentClimbable != null && Mathf.Abs(currentClimbable.position.x - transform.position.x) > manager.tileSize * 2) {
			// this is a hack to make sure the climbable is nullified if the player is nowhere near it.
			// for some reason, sometimes OnTriggerExit doesn't get called so the reference stays.
			currentClimbable = null;
		}

		if (currentClimbable != null) {
			if (controlManager.GetUp(ControlState.IsPressed)) {
				if (!isClimbing) {
					climbStartPos = transform.position;
					climbStartTime = Time.time;
				}
				
				isClimbing = true;
			}
			
			if (isClimbing) {
				//if (!animator.GetCurrentAnimatorStateInfo(0).IsName("PlayerClimb")) animator.Play(animationStateClimb);
				
				velocity.x = 0;
				velocity.y = 0;
				
				Vector3 goalPosCenter = new Vector3(currentClimbable.position.x, transform.position.y, transform.position.z);
				
				controller.move(Vector3.Lerp(climbStartPos, goalPosCenter, (Time.time - climbStartTime) / centeringSpeed) - transform.position);
				
				if (controlManager.GetUp(ControlState.IsPressed)) {
					velocity.y += climbSpeed;
				}
				if (controlManager.GetDown(ControlState.IsPressed)) {
					velocity.y -= climbSpeed;
				}
				
				BoxCollider2D climbableCollider = (BoxCollider2D)currentClimbable.collider2D;
				float maxClimbableY = currentClimbable.position.y + climbableCollider.size.y / 2f + climbableCollider.center.y;
				
				if (currentClimbable.tag == "ClimbableTop") {
					if (transform.position.y >= maxClimbableY) {
						velocity.y = Mathf.Min(0, velocity.y);
						
						Vector3 goalPosTop = new Vector3(transform.position.x, maxClimbableY, transform.position.z);
						controller.move(Vector3.Lerp(climbStartPos, goalPosTop, (Time.time - climbStartTime) / centeringSpeed) - transform.position);
					}
				}
			}
		}
	}

	void OnTriggerEnter2D(Collider2D coll) {
		AbstractEnemy enemy = coll.GetComponent<AbstractEnemy>();
		
		if (enemy) IntersectWithEnemy(enemy);
	}

	public void IntersectWithEnemy(AbstractEnemy enemy) {
		if (isFlashing) return;

		// landed on top
		if (!controller.isGrounded && controller.velocity.y < 0) {
			if (enemy.isJumpable) {
				AddExternalForce(new Vector3(0, enemy.jumpoff, 0));
				Jump();
				enemy.HitWithPlayerFeet(this);
			}
		}

		// touched from side or underneath
		else {
			InfectBy(enemy.infectionSpread);

			float side = Mathf.Sign(transform.position.x - enemy.transform.position.x);

			AddExternalForce(new Vector3(side * enemy.pushback, 0, 0));

			BeginFlashing(0.75f);
		}
	}

	void UpdateFlashing() {
		if (!isFlashing) return;

		if (Time.time - flashingStartTime > flashingLength) {
			isFlashing = false;
			animator.SetTrigger("Stop Flashing");
		}
	}

	void BeginFlashing(float flashingLength) {
		if (isFlashing) return;

		animator.SetTrigger("Start Flashing");

		this.flashingLength = flashingLength;

		flashingStartTime = Time.time;

		isFlashing = true;
	}

	void InfectBy(float infectionAmount) {
		if (Time.time - lastDamageTime < postDamageCoolDown) return;

		lastDamageTime = Time.time;

		infection = Mathf.Min(infection + infectionAmount, 100);
		if (infection == 100) Kill();
	}

	void UpdateJumpingAndFalling(ref Vector3 velocity) {
		bool shouldJump = shouldJumpNextFrame ||
			((controller.isGrounded || isClimbing || Time.time - timeOfBeginFall < airJumpTime) && controlManager.GetJump(ControlState.WasPressed));

		if (shouldJump) {
			if (controlManager.GetJump(ControlState.WasPressed)) lastActionWasJump = true;
			else lastActionWasJump = false;

			isClimbing = false;
			shouldJumpNextFrame = false;
			
			animator.SetTrigger("Jump");
			
			velocity.y = Mathf.Sqrt(2f * jumpHeight * -manager.gravity * (isClimbing?0.5f:1));
		}

		// cut jump short if you release space early
		if (lastActionWasJump) {
			if (controlManager.GetJump(ControlState.WasReleased)) {
				velocity.y *= 0.35f;
			}
		}

		if (controller.isGrounded) animator.SetBool("isGrounded", true);
		else animator.SetBool("isGrounded", false);

		if (controller.collisionState.becameUngroundedThisFrame) timeOfBeginFall = Time.time;

		animator.SetFloat("timeFalling", Time.time - timeOfBeginFall);
	}

	void ApplyGravity(ref Vector3 velocity) {
		velocity.y += manager.gravity * Time.deltaTime;
	}

	public void AddExternalForce(Vector3 force) {
		outsideForce += force;
	}

	void ApplyExternalForce(ref Vector3 velocity) {
		velocity += outsideForce;
		outsideForce = Vector3.zero;
	}

	IEnumerator TemporarilyTurnOffGroundCollisions(float time) {
		originalPlatformMask = controller.platformMask;
		controller.platformMask = 0;

		yield return new WaitForSeconds(time);

		controller.platformMask = originalPlatformMask;
	}

	void HandleControllerCollidedEvent(RaycastHit2D raycastHit) {
		if (controller.isGrounded) {
			isClimbing = false;
			lastActionWasJump = false;
			currentGroundTile = raycastHit.collider.transform;
			timeOfBeginFall = Time.time;

			animator.SetBool("isGrounded", true);
		}
		else {
			currentGroundTile = null;
			animator.SetBool("isGrounded", false);
		}
	}

	void Jump() {
		if (shouldJumpNextFrame) return;

		shouldJumpNextFrame = true;
	}

	void Kill() {
		if (isDead) return;

		controller.usePhysicsForMovement = true;

		isDead = true;
	}
}
