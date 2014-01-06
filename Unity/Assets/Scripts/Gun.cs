using UnityEngine;
using System.Collections;

public class Gun : MonoBehaviour {
	public GunType gunType {get; private set;}
	public GameObject bulletPrefab;

	[HideInInspector] public GunHolder currentGunHolder = null;
	[HideInInspector] public SpriteRenderer spriteRenderer;

	protected Manager manager;

	void Awake() {
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
	}

	public void SetupAsGunType(GunType gunType) {
		this.gunType = gunType;

		if (gunType == GunType.Pistol) spriteRenderer.color = Color.white;
		else if (gunType == GunType.PistolBlue) spriteRenderer.color = Color.blue;
		else if (gunType == GunType.PistolGreen) spriteRenderer.color = Color.green;
		else if (gunType == GunType.PistolRed) spriteRenderer.color = Color.red;
	}

	void OnTriggerEnter2D(Collider2D coll) {
		if (coll.tag == "Player") {
			Player player = coll.GetComponent<Player>();
			player.GetComponentInChildren<GunHolder>().PickupGun(this);
		}
	}
}
