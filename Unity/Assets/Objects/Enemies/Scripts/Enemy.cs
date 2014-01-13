using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController2D))]

public class Enemy : MonoBehaviour {
	public EnemyType enemyType = EnemyType.NONE;
	public AudioClip killSound;
	public float gravityMultiplier = 1;

	[HideInInspector] public CharacterController2D controller;

	protected Manager manager;

	void Awake() {
		controller = GetComponent<CharacterController2D>();
		controller.onControllerCollidedEvent += HandleControllerCollidedEvent;
		manager = GameObject.Find("Manager").GetComponent<Manager>();
	}

	void Start() {

	}
	
	void Update() {

	}

	protected void ApplyGravity() {
		float vel = controller.velocity.y + manager.gravity * gravityMultiplier * Time.deltaTime;

		controller.move(new Vector3(0, vel * Time.deltaTime, 0));
	}
	
	virtual public void Kill() {
		GameObject.Destroy(this.gameObject);
		AudioSource.PlayClipAtPoint(killSound, Vector3.zero);
	}

	virtual public void HitWithBullet(Bullet bullet) {

	}

	virtual public void HitWithPlayerFeet(Player player) {

	}

	virtual public void HandleControllerCollidedEvent(RaycastHit2D raycastHit) {

	}
}
