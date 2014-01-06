using UnityEngine;
using System.Collections;

public class GunHolder : MonoBehaviour {
	public Vector3 localGunAttachPos = Vector3.zero;
	public float delayAfterGunPickup = 1;
	public AudioClip pickupSound;

	[HideInInspector] public Direction facingDirection = Direction.Right;
	[HideInInspector] public Gun currentGun = null;

	protected float timeOfLastGunPickup = 0;

	void Awake() {

	}

	void Start() {
	
	}
	
	void Update() {
	
	}

	public void PickupGun(Gun gun) {
		if (Time.time - timeOfLastGunPickup < delayAfterGunPickup) return;

		if (currentGun != null) {
			if (gun.gunType == currentGun.gunType) return;
			else DropGun();
		}

		AudioSource.PlayClipAtPoint(pickupSound, Vector3.zero);

		timeOfLastGunPickup = Time.time;
		currentGun = gun;
		gun.currentGunHolder = this;
		gun.transform.parent = transform;
		gun.transform.localScale = new Vector3(1, 1, 1);
		gun.transform.localPosition = localGunAttachPos;
	}
	
	public void DropGun() {
		if (currentGun == null) return;
		
		currentGun.transform.parent = null;
		currentGun = null;
	}
}
