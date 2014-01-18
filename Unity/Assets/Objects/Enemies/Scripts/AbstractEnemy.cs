using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController2D))]

public class AbstractEnemy : MonoBehaviour {
	public AudioClip killSound;

	public bool shouldGoOverLedges = false;
	public bool isJumpable = true;

	public float gravityMultiplier = 1;
	public float health = 3;
	public float infectionSpread = 10;
	public float pushback = 5;
	public float jumpoff = 2;

	[HideInInspector] public EnemyType enemyType = EnemyType.NONE;
	[HideInInspector] public CharacterController2D controller;

	protected Manager manager;

	virtual public void Awake() {
		controller = GetComponent<CharacterController2D>();
		controller.onControllerCollidedEvent += HandleControllerCollidedEvent;
		manager = GameObject.Find("Manager").GetComponent<Manager>();
	}

	virtual public void Start() {

	}
	
	virtual public void Update() {
		throw new UnityException("haven't overridden update in " + this.name);
	}

	virtual public void HitWithBullet(Bullet bullet) {
		DamageBy(bullet.power);
	}

	virtual public void HitWithPlayerFeet(Player player) {
		DamageBy(player.footPower);
	}

	virtual protected void DamageBy(float damageAmount) {
		health = Mathf.Max(health - damageAmount, 0);
		
		if (health == 0) Kill();
	}

	protected void ApplyGravity(ref Vector3 velocity) {
		velocity.y += manager.gravity * gravityMultiplier * Time.deltaTime;
	}

	virtual public void Kill() {
		GameObject.Destroy(this.gameObject);
		AudioSource.PlayClipAtPoint(killSound, Vector3.zero);
	}

	virtual public void HandleControllerCollidedEvent(RaycastHit2D raycastHit) {

	}
}
