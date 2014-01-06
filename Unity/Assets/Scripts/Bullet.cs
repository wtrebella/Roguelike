using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float gravityMultiplier = 1;
	public BulletType bulletType;

	protected Direction direction;
	protected Vector3 velocity;
	protected Gun gun;
	protected Manager manager;
	protected bool hasBeenSetup = false;
	protected bool hasShot = false;

	void Awake() {
		manager = GameObject.Find("Manager").GetComponent<Manager>();
	}

	void Start () {

	}
	
	void Update () {
		if (!hasBeenSetup || !hasShot) return;

		velocity.y += manager.gravity * gravityMultiplier * Time.deltaTime;

		transform.position += velocity * Time.deltaTime;

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

		direction = gun.currentGunHolder.facingDirection;
		transform.position = gun.transform.position;
		velocity = gun.shootVelocity;

		int dirMultiplier = direction == Direction.Right?1:-1;
		Vector3 exitPointDelta = gun.bulletExitPoint;

		exitPointDelta.x *= dirMultiplier;
		velocity.x *= dirMultiplier;

		velocity = Quaternion.Euler(0, 0, Random.Range(-gun.angleOfSpread / 2f, gun.angleOfSpread / 2f)) * velocity;

		transform.position += exitPointDelta;

		hasBeenSetup = true;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			coll.GetComponent<GroundTile>().Destroy();
			Destroy();
		}
	}
}
