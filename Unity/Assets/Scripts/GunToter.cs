using UnityEngine;
using System.Collections;

public class GunToter : MonoBehaviour {
	public Vector3 localGunAttachPos = Vector3.zero;
	public float delayAfterGunPickup = 1;

	protected float timeOfLastGunPickup = 0;
	protected Gun currentGun = null;

	void Start() {
	
	}
	
	void Update() {
	
	}

	public void PickupGun(Gun gun) {
		if (Time.time - timeOfLastGunPickup < delayAfterGunPickup) return;

		if (currentGun != null) DropGun();

		timeOfLastGunPickup = Time.time;
		currentGun = gun;
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
