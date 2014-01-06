using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	public GunType gunType;
	public GameObject bulletPrefab;
	public Vector3 shootVelocity;
	public Vector3 bulletExitPoint;
	public float angleOfSpread = 0;
	public float screenShakeIntensity = 0;
	public float screenShakeDecayTime = 0;
	public AudioClip shootSound;

	[HideInInspector] public GunHolder currentGunHolder = null;
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

	public void Shoot() {
		Bullet bullet = ((GameObject)Instantiate(bulletPrefab)).GetComponent<Bullet>();
		bullet.SetGun(this);
		bullet.Shoot();
		AudioSource.PlayClipAtPoint(shootSound, Vector3.zero);
		Camera.main.GetComponent<CameraShake>().Shake(screenShakeIntensity, screenShakeDecayTime);
		if (animator != null) animator.SetTrigger("Shoot");
	}

	void OnTriggerEnter2D(Collider2D coll) {
		GunHolder otherGunHolder = coll.GetComponentInChildren<GunHolder>();
		if (otherGunHolder != null) otherGunHolder.PickupGun(this);
	}
}
