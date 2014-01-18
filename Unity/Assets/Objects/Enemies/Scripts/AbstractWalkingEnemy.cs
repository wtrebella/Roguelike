using UnityEngine;
using System.Collections;

[RequireComponent (typeof(ObjectFlipper))]

public class AbstractWalkingEnemy : AbstractEnemy {
	public float moveSpeed = 3;
	public float moveSpeedVariation = 1;

	protected float timeLastTriedToFlip = 0;
	protected float flipTryCoolDown = 0.3f;
	protected bool isMovingRight = false;
	
	public override void Start() {
		base.Start();
		
		moveSpeed += Random.Range(-moveSpeedVariation, moveSpeedVariation);
	}
	
	public override void Update() {
		UpdateLedgeCases();
		
		Vector3 velocity = new Vector3(isMovingRight?moveSpeed:-moveSpeed, controller.velocity.y, 0);
		
		ApplyGravity(ref velocity);
		
		controller.move(velocity * Time.deltaTime);
	}

	virtual public void UpdateLedgeCases() {
		if (!shouldGoOverLedges) {
			float hoverTestDist = 0.5f;
			
			if (!isMovingRight && controller.LeftSideIsHovering(hoverTestDist)) Flip();
			if (isMovingRight && controller.RightSideIsHovering(hoverTestDist)) Flip();
		}
	}
	
	override public void HandleControllerCollidedEvent(RaycastHit2D raycastHit) {
		base.HandleControllerCollidedEvent(raycastHit);
		
		if (controller.isGrounded && ((isMovingRight && controller.collisionState.right) || (!isMovingRight && controller.collisionState.left))) {
			Flip();
		}
	}

	void Flip() {
		if (Time.time - timeLastTriedToFlip < flipTryCoolDown) {
			timeLastTriedToFlip = Time.time;
		}
		else {
			isMovingRight = !isMovingRight;
			GetComponent<ObjectFlipper>().Flip();
			timeLastTriedToFlip = Time.time;
		}
	}
}
