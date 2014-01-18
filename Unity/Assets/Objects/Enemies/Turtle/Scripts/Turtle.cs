using UnityEngine;
using System.Collections;

public class Turtle : AbstractWalkingEnemy {
	public override void Awake() {
		base.Awake();

		enemyType = EnemyType.Turtle;
	}

	public override void Update() {
		UpdateLedgeCases();

		Vector3 velocity = new Vector3(isMovingRight?moveSpeed:-moveSpeed, controller.velocity.y, 0);

		ApplyGravity(ref velocity);

		controller.move(velocity * Time.deltaTime);
	}
}
