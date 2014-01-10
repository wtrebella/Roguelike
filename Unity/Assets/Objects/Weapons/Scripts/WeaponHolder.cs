using UnityEngine;
using System.Collections;

public class WeaponHolder : MonoBehaviour {
	public Vector3 localWeaponAttachPos = Vector3.zero;
	public float delayAfterWeaponPickup = 1;
	public AudioClip pickupSound;

	[HideInInspector] public Player weaponOwner;
	[HideInInspector] public Direction facingDirection = Direction.Right;
	[HideInInspector] public Weapon currentWeapon = null;

	protected float timeOfLastWeaponPickup = -float.MaxValue;

	void Awake() {
		weaponOwner = transform.parent.GetComponent<Player>();
	}

	void Start() {
	
	}
	
	void Update() {
	
	}

	public void PickupWeapon(Weapon weapon, bool playSound = true) {
		if (Time.time - timeOfLastWeaponPickup < delayAfterWeaponPickup) return;

		if (currentWeapon != null) {
			if (weapon.weaponType == currentWeapon.weaponType) return;
			else DropWeapon();
		}

		if (playSound) AudioSource.PlayClipAtPoint(pickupSound, Vector3.zero);

		timeOfLastWeaponPickup = Time.time;
		currentWeapon = weapon;
		weapon.currentWeaponHolder = this;
		weapon.transform.parent = transform;
		weapon.transform.localScale = new Vector3(1, 1, 1);
		weapon.transform.localPosition = localWeaponAttachPos;
		weapon.transform.localRotation = Quaternion.identity;
	}
	
	public void DropWeapon() {
		if (currentWeapon == null) return;

		currentWeapon.transform.parent = null;
		currentWeapon = null;
	}
}
