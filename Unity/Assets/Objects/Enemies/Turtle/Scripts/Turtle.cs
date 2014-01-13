using UnityEngine;
using System.Collections;

public class Turtle : Enemy {
	void Start() {

	}
	
	void LateUpdate() {
		ApplyGravity();
	}

	override public void HitWithBullet(Bullet bullet) {
		Kill();
	}

	override public void HitWithPlayerFeet(Player player) {
		Kill();
	}
}
