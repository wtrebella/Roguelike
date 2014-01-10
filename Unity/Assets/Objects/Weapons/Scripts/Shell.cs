using UnityEngine;
using System.Collections;

public class Shell : MonoBehaviour {
	public float lengthOfExistence = 3;
	public float lengthOfFade = 0.5f;

	protected Weapon gun;
	protected bool hasBeenSetup = false;
	protected bool hasBegunFade = false;
	protected bool hasShotOut = false;
	protected float timeOfBeginFade = 0;
	protected float timeOfInstantiation = 0;

	// Use this for initialization
	void Start () {
		transform.rotation = Quaternion.Euler(0, 0, Random.Range(0, 360));
		timeOfInstantiation = Time.time;
	}

	public void ShootOut() {
		if (!hasBeenSetup) throw new UnityException("gun hasn't been set up!");

		transform.position = gun.shellExitTransform.position;

		hasShotOut = true;
		rigidbody2D.velocity = new Vector2(Random.Range(-gun.shellVelocityMultiplier, gun.shellVelocityMultiplier), Random.Range(-gun.shellVelocityMultiplier, gun.shellVelocityMultiplier));
		rigidbody2D.angularVelocity = Random.Range(-gun.shellAngularVelocityMultiplier, gun.shellAngularVelocityMultiplier);
	}

	public void SetWeapon(Weapon gun) {
		this.gun = gun;

		hasBeenSetup = true;
	}

	// Update is called once per frame
	void Update () {
		if (!hasShotOut || !hasBeenSetup) return;

		if (Time.time - timeOfInstantiation > lengthOfExistence) {
			if (!hasBegunFade) {
				hasBegunFade = true;
				timeOfBeginFade = Time.time;
			}
			SpriteRenderer sr = GetComponent<SpriteRenderer>();
			sr.color = Color.Lerp(Color.white, new Color(1, 1, 1, 0), (Time.time - timeOfBeginFade) / lengthOfFade);
			if (sr.color.a == 0) GameObject.Destroy(this.gameObject);
		}
	}
}
