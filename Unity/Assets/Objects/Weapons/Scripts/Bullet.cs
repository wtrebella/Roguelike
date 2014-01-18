using UnityEngine;
using System.Collections;

public class Bullet : MonoBehaviour {
	//public float gravityMultiplier = 1;
	public float rotationSpeed = 0;
	public float power = 1;
	public BulletType bulletType;
	public GameObject bulletExplosionPrefab;
	public AudioClip woodHitSound;

	protected GameObject bulletExplosion;
	protected bool isDead = false;
	protected Direction direction;
	//protected Vector3 velocity;
	protected Weapon gun;
	protected Manager manager;
	protected bool hasBeenSetup = false;
	protected bool hasShot = false;
	protected ParticleSystem particleTrail;

	void Awake() {
		particleTrail = GetComponentInChildren<ParticleSystem>();
		manager = GameObject.Find("Manager").GetComponent<Manager>();
	}

	void Start () {
		transform.rotation = Quaternion.Euler(new Vector3(0, 0, Random.Range(0, 360)));
	}
	
	void Update () {
		if (!hasBeenSetup || !hasShot || isDead) return;

		//velocity.y += manager.gravity * gravityMultiplier * Time.deltaTime;

		//transform.position += velocity * Time.deltaTime;
		transform.Rotate(new Vector3(0, 0, rotationSpeed * Time.deltaTime));
		
		if ((transform.position - gun.transform.position).magnitude > 50) Kill();
	}

	void FixedUpdate() {
		float velocityAngle = Mathf.Atan2(rigidbody2D.velocity.y, rigidbody2D.velocity.x) * Mathf.Rad2Deg;
		particleTrail.transform.rotation = Quaternion.Euler(velocityAngle, 270, 0);
	}

	void Kill() {
		bulletExplosion = (GameObject)Instantiate(bulletExplosionPrefab, transform.position, Quaternion.identity);

		isDead = true;

		GetComponentInChildren<SpriteRenderer>().enabled = false;
		particleTrail.Stop();

		StartCoroutine(WaitThenDestroy());
	}

	IEnumerator WaitThenDestroy() {
		yield return new WaitForSeconds(2.0f);

		GameObject.Destroy(bulletExplosion);
		GameObject.Destroy(this.gameObject);
	}

	public void Shoot() {
		if (!hasBeenSetup) throw new UnityException("gun hasn't been set up!");

		particleTrail.Play();
		direction = gun.currentWeaponHolder.facingDirection;
		transform.position = gun.bulletExitTransform.position;
		Vector3 newVelocity = gun.shootVelocity;

		int dirMultiplier = direction == Direction.Right?1:-1;
		
		newVelocity.x *= dirMultiplier;

		float spreadAngle = gun.baseSpreadAngle;
		float recoilSpreadMultiplier = Mathf.Max(0, (1 - (Time.time - gun.timeOfLastShot) / gun.recoilTime)) * gun.recoilSpreadMultiplier + 1;
		if (recoilSpreadMultiplier > 0 && spreadAngle == 0) spreadAngle = 1;
		spreadAngle *= recoilSpreadMultiplier;

		newVelocity = Quaternion.Euler(0, 0, Random.Range(-spreadAngle / 2f, spreadAngle / 2f)) * newVelocity;

		rigidbody2D.velocity = newVelocity;

		hasShot = true;
	}

	public void SetWeapon(Weapon gun) {
		this.gun = gun;

		hasBeenSetup = true;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (isDead) return;

		GroundTile groundTile = coll.GetComponent<GroundTile>();

		if (groundTile != null) {
			groundTile.HitWithBullet(this);
			Kill();
			AudioSource.PlayClipAtPoint(woodHitSound, Vector3.zero);
		}

		AbstractEnemy enemy = coll.GetComponent<AbstractEnemy>();
		if (enemy) {
			enemy.HitWithBullet(this);
			Kill();
		}
	}
}
