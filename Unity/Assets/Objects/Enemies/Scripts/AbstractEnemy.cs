using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController2D))]

public class AbstractEnemy : MonoBehaviour {
	public AudioClip killSound;
	public float gravityMultiplier = 1;
	public bool shouldGoOverLedges = false;

	[HideInInspector] public EnemyType enemyType = EnemyType.NONE;
	[HideInInspector] public CharacterController2D controller;

	protected Manager manager;

	public virtual void Awake() {
		controller = GetComponent<CharacterController2D>();
		controller.onControllerCollidedEvent += HandleControllerCollidedEvent;
		manager = GameObject.Find("Manager").GetComponent<Manager>();
	}

	public virtual void Start() {

	}
	
	public virtual void Update() {
	
	}

	protected void ApplyGravity(ref Vector3 velocity) {
		velocity.y += manager.gravity * gravityMultiplier * Time.deltaTime;
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
