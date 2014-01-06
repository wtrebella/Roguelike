using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public BulletType bulletType;
	public Direction direction;

	protected float gravityMultiplier;
	protected Vector3 velocity;
	protected Gun gun;
	protected Manager manager;
	protected bool hasBeenSetup = false;
	protected bool hasShot = false;

	void Awake() {
		manager = GameObject.Find("Manager").GetComponent<Manager>();
	}

	void Start () {
		if (direction == Direction.Left) velocity *= -1;
	}
	
	void Update () {
		if (!hasBeenSetup || !hasShot) return;

		if (bulletType == BulletType.Pistol) {
			velocity.y += manager.gravity * gravityMultiplier * Time.deltaTime;

			transform.position += velocity * Time.deltaTime;
		}

		if ((transform.position - gun.transform.position).magnitude > 50) Destroy();
	}

	void Destroy() {
		GameObject.Destroy(this.gameObject);
	}

	public void Shoot() {
		if (!hasBeenSetup) throw new UnityException("gun hasn't been set up!");

		hasShot = true;
	}

	public void SetGun(Gun gun) {
		this.gun = gun;

		GetComponent<SpriteRenderer>().color = gun.spriteRenderer.color;
		direction = gun.currentGunHolder.facingDirection;
		transform.position = gun.transform.position;
		int dirMultiplier = direction == Direction.Right?1:-1;
		Vector3 exitPointDelta = Vector3.zero;

		if (gun.gunType == GunType.Pistol) {
			gravityMultiplier = manager.pistolBulletGravityMultiplier;
			velocity = manager.pistolShootVelocity;
			exitPointDelta = manager.pistolBulletExitPoint;
		}

		else if (gun.gunType == GunType.PistolBlue) {
			gravityMultiplier = manager.pistolBlueBulletGravityMultiplier;
			velocity = manager.pistolBlueShootVelocity;
			exitPointDelta = manager.pistolBlueBulletExitPoint;
		}

		else if (gun.gunType == GunType.PistolGreen) {
			gravityMultiplier = manager.pistolGreenBulletGravityMultiplier;
			velocity = manager.pistolGreenShootVelocity;
			exitPointDelta = manager.pistolGreenBulletExitPoint;
		}

		else if (gun.gunType == GunType.PistolRed) {
			gravityMultiplier = manager.pistolRedBulletGravityMultiplier;
			velocity = manager.pistolRedShootVelocity;
			exitPointDelta = manager.pistolRedBulletExitPoint;
		}

		exitPointDelta.x *= dirMultiplier;
		
		transform.position += exitPointDelta;

		hasBeenSetup = true;
	}
}
