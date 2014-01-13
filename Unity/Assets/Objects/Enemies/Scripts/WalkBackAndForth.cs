using UnityEngine;
using System.Collections;

[RequireComponent (typeof(CharacterController2D))]

public class WalkBackAndForth : MonoBehaviour {
	public bool shouldGoOverLedges = false;
	public float moveSpeed = 10;
	public float moveSpeedVariation = 0;

	[HideInInspector] public CharacterController2D controller;

	protected Manager manager;
	protected bool isMovingRight = false;

	// Use this for initialization
	void Start () {
		controller = GetComponent<CharacterController2D>();
		controller.onControllerCollidedEvent += HandleControllerCollidedEvent;
		manager = GameObject.Find("Manager").GetComponent<Manager>();

		moveSpeed += Random.Range(-moveSpeedVariation, moveSpeedVariation);
		
		if (Random.value < 0.5f) Flip();
	}
	
	// Update is called once per frame
	void Update () {
		if (!shouldGoOverLedges) {
			float hoverTestDist = 0.5f;
			
			if (!isMovingRight && controller.LeftSideIsHovering(hoverTestDist)) Flip();
			if (isMovingRight && controller.RightSideIsHovering(hoverTestDist)) Flip();
		}

		float xVel = isMovingRight?moveSpeed:-moveSpeed;

		controller.move(new Vector3(xVel * Time.deltaTime, 0, 0));
	}

	protected void Flip() {
		isMovingRight = !isMovingRight;
		GetComponent<ObjectFlipper>().Flip();
	}

	virtual public void HandleControllerCollidedEvent(RaycastHit2D raycastHit) {
		if ((isMovingRight && controller.collisionState.right) || (!isMovingRight && controller.collisionState.left)) {
			Flip();
		}
	}
}
