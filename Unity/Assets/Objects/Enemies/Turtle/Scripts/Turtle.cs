using UnityEngine;
using System.Collections;

public class Turtle : Enemy {
	public float approximateMoveSpeed = 10;
	public float moveSpeedVariation = 0;

	bool isMovingRight = false;

	void Start() {
		approximateMoveSpeed += Random.Range(-moveSpeedVariation, moveSpeedVariation);
	}
	
	void Update() {
		Vector3 velocity = new Vector3(isMovingRight?approximateMoveSpeed:-approximateMoveSpeed, rigidbody2D.velocity.y, 0);

		ApplyGravity(ref velocity);

		controller.move(velocity * Time.deltaTime);
	}

	override public void HitWithBullet(Bullet bullet) {
		Kill();
	}

	override public void HitWithPlayerFeet(Player player) {
		Kill();
	}

	override public void HandleControllerCollidedEvent(RaycastHit2D raycastHit) {
		if ((isMovingRight && controller.collisionState.right) || (!isMovingRight && controller.collisionState.left)) {
			isMovingRight = !isMovingRight;
			GetComponent<ObjectFlipper>().Flip();
		}
	}

//	void OnCollisionEnter2D(Collision2D coll) {
//		RaycastHit2D[] raycastHitsLeft = controller.Raycast(Direction.Left, 5, LayerMask.NameToLayer("Ground"));
//		RaycastHit2D[] raycastHitsRight = controller.Raycast(Direction.Right, 5, LayerMask.NameToLayer("Ground"));
//
//		foreach (RaycastHit2D r in raycastHitsLeft) {
//			if (r.collider == coll.collider) {
//				GetComponent<ObjectFlipper>().Flip();
//				isMovingRight = !isMovingRight;
//			}
//		}
//
//		foreach (RaycastHit2D r in raycastHitsRight) {
//			if (r.collider == coll.collider) {
//				GetComponent<ObjectFlipper>().Flip();
//				isMovingRight = !isMovingRight;
//			}
//		}
//	}
}
