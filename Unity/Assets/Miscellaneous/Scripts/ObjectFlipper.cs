using UnityEngine;
using System.Collections;

public class ObjectFlipper : MonoBehaviour {
	public Transform spriteObject;

	[HideInInspector] public Direction facingDirection = Direction.Right;

	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void Face(Direction dir) {
		if (facingDirection == dir) return;
		
		facingDirection = dir;

		float newScaleX = Mathf.Abs(spriteObject.localScale.x);
		if (dir == Direction.Left) newScaleX *= -1;

		spriteObject.localScale = new Vector3(newScaleX, spriteObject.localScale.y, spriteObject.localScale.z);
	}

	public void Flip() {
		Direction dir = facingDirection;
		if (dir == Direction.Right) dir = Direction.Left;
		else if (dir == Direction.Left) dir = Direction.Right;

		Face(dir);
	}
}
