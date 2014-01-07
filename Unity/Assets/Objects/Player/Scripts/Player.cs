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
	public float centeringSpeed = 0.3f;

	[HideInInspector] public Transform currentClimbable;
	[HideInInspector] public Direction facingDirection = Direction.Right;

	protected ControlManager controlManager;
	protected Manager manager;
	protected GunHolder gunHolder;
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
//	protected Animator animator;
//	protected int animationStateWalk;
//	protected int animationStateStand;
//	protected int animationStateJump;
//	protected int animationStateClimb;
	protected bool previouslyWasGrounded = true;

	void Awake() {
		controlManager = GameObject.Find("Control Manager").GetComponent<ControlManager>();
		controller = GetComponent<CharacterController2D>();
		controller.onControllerCollidedEvent += HandleControllerCollidedEvent;
		manager = GameObject.Find("Manager").GetComponent<Manager>();
		gunHolder = GetComponentInChildren<GunHolder>();
		animator = GetComponent<Animator>();

//		animationStateWalk = Animator.StringToHash("PlayerWalk");
//		animationStateStand = Animator.StringToHash("PlayerStand");
//		animationStateJump = Animator.StringToHash("PlayerJump");
//		animationStateClimb = Animator.StringToHash("PlayerClimb");
//
//		animator = playerSpriteObject.GetComponent<Animator>();
	}

	void Start() {

	}

	void Update() {
		UpdateGun();

		Vector3 velocity = controller.velocity;
		
		ApplyDrag(ref velocity);
		UpdateRunning(ref velocity);
		UpdateClimbing(ref velocity);
		UpdateJumpingAndFalling(ref velocity);
		ApplyGravity(ref velocity);
		ApplyOutsideForce(ref velocity);

		controller.move(velocity * Time.deltaTime);
		
		previouslyWasGrounded = controller.isGrounded;
	}

	void UpdateGun() {
		gunHolder.facingDirection = facingDirection;
		Gun gun = gunHolder.currentGun;
		if (gun == null) return;

		bool shouldShoot = gun.isAutomaticFire && controlManager.GetShoot(ControlState.IsPressed) && gun.CanShoot();
		shouldShoot = shouldShoot || (!gun.isAutomaticFire && controlManager.GetShoot(ControlState.WasPressed));

		if (shouldShoot) gun.Shoot();
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

			Face(Direction.Right);
		}
		else if (controlManager.GetLeft(ControlState.IsPressed)) {
			if (controller.isGrounded) animator.SetBool("isRunning", true);

			velocity.x = Mathf.Max(velocity.x - runSpeed, -runSpeed);

			Face(Direction.Left);
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

	void UpdateJumpingAndFalling(ref Vector3 velocity) {
		if ((controller.isGrounded || isClimbing) && controlManager.GetJump(ControlState.WasPressed)) {
			lastActionWasJump = true;
			isClimbing = false;

			animator.SetTrigger("Jump");
			
			if ((controlManager.GetDown(ControlState.IsPressed) && currentGroundTile != null) && currentGroundTile.gameObject.layer == LayerMask.NameToLayer("OneWayGround")) {
				velocity.y = 0;
				StartCoroutine(TemporarilyTurnOffGroundCollisions(0.05f));
			}
			else {
				velocity.y = Mathf.Sqrt(2f * jumpHeight * -manager.gravity * (isClimbing?0.5f:1));
			}
		}

		// cut jump short if you release space early
		if (lastActionWasJump) {
			if (controlManager.GetJump(ControlState.WasReleased)) {
				velocity.y *= 0.35f;
			}
		}

		if (controller.isGrounded) animator.SetBool("isGrounded", true);
		else animator.SetBool("isGrounded", false);

		if (previouslyWasGrounded && !controller.isGrounded) timeOfBeginFall = Time.time;

		animator.SetFloat("timeFalling", Time.time - timeOfBeginFall);
	}

	void ApplyGravity(ref Vector3 velocity) {
		velocity.y += manager.gravity * Time.deltaTime;
	}

	public void AddOutsideForce(Vector3 force) {
		outsideForce += force;
	}

	void ApplyOutsideForce(ref Vector3 velocity) {
		if (outsideForce.y > 0 && !controller.isGrounded) outsideForce.y = 0;
		velocity += outsideForce;
		outsideForce = Vector3.zero;
	}

	IEnumerator TemporarilyTurnOffGroundCollisions(float time) {
		originalPlatformMask = controller.platformMask;
		controller.platformMask = 0;

		yield return new WaitForSeconds(time);

		controller.platformMask = originalPlatformMask;
	}

	void Face(Direction dir) {
		if (facingDirection == dir) return;

		facingDirection = dir;
		transform.localScale = new Vector3(-transform.localScale.x, transform.localScale.y, transform.localScale.z);
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

}
