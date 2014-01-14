using UnityEngine;
using System.Collections;

public class Rat : AbstractEnemy {
	public float moveSpeed = 3;
	public float moveSpeedVariation = 1;
	
	bool isMovingRight = false;
	
	public override void Awake() {
		base.Awake();
		
		enemyType = EnemyType.Rat;
	}
	
	public override void Start() {
		base.Start();
		
		moveSpeed += Random.Range(-moveSpeedVariation, moveSpeedVariation);
	}
	
	public override void Update() {
		base.Update();
		
		if (!shouldGoOverLedges) {
			float hoverTestDist = 0.5f;
			
			if (!isMovingRight && controller.LeftSideIsHovering(hoverTestDist)) {
				isMovingRight = true;
				GetComponent<ObjectFlipper>().Flip();
			}
			if (isMovingRight && controller.RightSideIsHovering(hoverTestDist)) {
				isMovingRight = false;
				GetComponent<ObjectFlipper>().Flip();
			}
		}
		
		Vector3 velocity = new Vector3(isMovingRight?moveSpeed:-moveSpeed, controller.velocity.y, 0);
		
		ApplyGravity(ref velocity);
		
		controller.move(velocity * Time.deltaTime);
	}
	
	override public void HitWithBullet(Bullet bullet) {
		base.HitWithBullet(bullet);
		
		Kill();
	}
	
	override public void HitWithPlayerFeet(Player player) {
		base.HitWithPlayerFeet(player);
		
		Kill();
	}
	
	override public void HandleControllerCollidedEvent(RaycastHit2D raycastHit) {
		base.HandleControllerCollidedEvent(raycastHit);
		
		if (controller.isGrounded && ((isMovingRight && controller.collisionState.right) || (!isMovingRight && controller.collisionState.left))) {
			isMovingRight = !isMovingRight;
			GetComponent<ObjectFlipper>().Flip();
		}
	}
}
