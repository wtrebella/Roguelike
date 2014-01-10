using UnityEngine;
using System.Collections;

public class Weapon : MonoBehaviour {
	public WeaponType weaponType;
	public GameObject bulletPrefab;
	public GameObject shellPrefab;
	public float shellVelocityMultiplier = 10;
	public float shellAngularVelocityMultiplier = 10;
	public Vector3 shootVelocity;
	public float baseSpreadAngle = 0;
	public float screenShakeIntensity = 0;
	public float screenShakeDecayTime = 0;
	public float recoilTime = 0;
	public float recoilSpreadMultiplier = 1;
	public float automaticFireTime = -1;
	public Vector3 recoilForce;
	public AudioClip shootSound;
	public Transform bulletExitTransform;
	public Transform shellExitTransform;
	public bool isAutomaticFire = false;

	[HideInInspector] public float timeOfLastShot = 0;
	[HideInInspector] public WeaponHolder currentWeaponHolder = null;
	[HideInInspector] public SpriteRenderer spriteRenderer;

	protected Animator animator;
	protected Manager manager;

	void Awake() {
		animator = GetComponentInChildren<Animator>();
		spriteRenderer = GetComponentInChildren<SpriteRenderer>();
		manager = GameObject.Find("Manager").GetComponent<Manager>();
	}

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public bool CanShoot() {
		return !isAutomaticFire || automaticFireTime == -1 || (isAutomaticFire && automaticFireTime >= 0 && (Time.time - timeOfLastShot >= automaticFireTime));
	}

	public void Shoot() {
		Bullet bullet = ((GameObject)Instantiate(bulletPrefab)).GetComponent<Bullet>();
		bullet.SetWeapon(this);
		bullet.Shoot();

		Shell shell = ((GameObject)Instantiate(shellPrefab)).GetComponent<Shell>();
		shell.SetWeapon(this);
		shell.ShootOut();

		Vector3 actualRecoil = recoilForce;
		actualRecoil.x *= currentWeaponHolder.facingDirection == Direction.Left?1:-1;
		currentWeaponHolder.weaponOwner.AddExternalForce(actualRecoil);

		Camera.main.GetComponent<CameraShake>().Shake(screenShakeIntensity, screenShakeDecayTime);

		if (animator != null) animator.SetTrigger("Shoot");

		AudioSource.PlayClipAtPoint(shootSound, Vector3.zero);

		timeOfLastShot = Time.time;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		WeaponHolder otherWeaponHolder = coll.GetComponentInChildren<WeaponHolder>();
		if (otherWeaponHolder != null) otherWeaponHolder.PickupWeapon(this);
	}
}
