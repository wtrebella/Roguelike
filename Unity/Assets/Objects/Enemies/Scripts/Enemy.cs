using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {
	public EnemyType enemyType = EnemyType.NONE;
	public AudioClip killSound;

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

	protected void ApplyGravity(ref Vector3 velocity) {
		velocity.y += manager.gravity * Time.deltaTime;
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
