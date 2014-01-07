using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	public float gravityMultiplier = 1;
	public float rotationSpeed = 0;
	public BulletType bulletType;

	protected bool isDead = false;
	protected Direction direction;
	protected Vector3 velocity;
	protected Gun gun;
	protected Manager manager;
	protected bool hasBeenSetup = false;
	protected bool hasShot = false;
	protected ParticleSystem particles;

	void Awake() {
		particles = GetComponentInChildren<ParticleSystem>();
		manager = GameObject.Find("Manager").GetComponent<Manager>();
	}

	void Start () {
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
	}
	
	void Update () {
		if (!hasBeenSetup || !hasShot || isDead) return;

		velocity.y += manager.gravity * gravityMultiplier * Time.deltaTime;

		transform.position += velocity * Time.deltaTime;
		transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));

		float velocityAngle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
		particles.transform.rotation = Quaternion.Euler(velocityAngle, 270, 0);

		if ((transform.position - gun.transform.position).magnitude > 50) Kill();
	}

	void Kill() {
		isDead = true;

		GetComponentInChildren<SpriteRenderer>().enabled = false;
		particles.Stop();

		StartCoroutine(WaitThenDestroy());
	}

	IEnumerator WaitThenDestroy() {
		yield return new WaitForSeconds(2.0f);

		GameObject.Destroy(this.gameObject);
	}

	public void Shoot() {
		if (!hasBeenSetup) throw new UnityException("gun hasn't been set up!");

		transform.position = gun.bulletExitTransform.position;
		particles.Play();
		direction = gun.currentGunHolder.facingDirection;
		transform.position = gun.transform.position;
		velocity = gun.shootVelocity;

		int dirMultiplier = direction == Direction.Right?1:-1;
		
		velocity.x *= dirMultiplier;

		float spreadAngle = gun.baseSpreadAngle;
		float recoilSpreadMultiplier = Mathf.Max(0, (1 - (Time.time - gun.timeOfLastShot) / gun.recoilTime)) * gun.recoilSpreadMultiplier + 1;
		if (recoilSpreadMultiplier > 0 && spreadAngle == 0) spreadAngle = 1;
		spreadAngle *= recoilSpreadMultiplier;

		velocity = Quaternion.Euler(0, 0, Random.Range(-spreadAngle / 2f, spreadAngle / 2f)) * velocity;
		hasShot = true;
	}

	public void SetGun(Gun gun) {
		this.gun = gun;

		hasBeenSetup = true;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (isDead) return;

		if (coll.gameObject.layer == LayerMask.NameToLayer("Ground")) {
			coll.GetComponent<GroundTile>().Destroy();
			Kill();
		}
	}
}
