﻿using UnityEngine;
using System.Collections;
using System;
using InControl;

public class Manager : MonoBehaviour {
	public float timeScale {
		get {return Time.timeScale;}
		set {Time.timeScale = value;}
	}

	public Vector3 drag = Vector3.zero;
	public float gravity = -50;
	public float tileSize = 0.7f;

	protected Player player;
	protected ControlManager controlManager;

	void Awake() {
		player = GameObject.Find("Player").GetComponent<Player>();
		controlManager = GameObject.Find("Control Manager").GetComponent<ControlManager>();
	}

	// Use this for initialization
	void Start () {
		InputManager.Setup();
	}

	// Update is called once per frame
	void Update () {
		InputManager.Update();
		
		if (player.isDead) {
			if (controlManager.GetEnter(ControlState.WasPressed)) {
				Application.LoadLevel("Main Scene");
			}
		}
	}

	public static Rect GetScreenRectInWorldPoints() {
		Vector3 screenOrigin = Camera.main.ViewportToScreenPoint(new Vector3(0, 0, 0));
		Vector3 screenExtents = Camera.main.ViewportToScreenPoint(new Vector3(1, 1,  0));

		Vector3 worldOrigin = Camera.main.ScreenToWorldPoint(screenOrigin);
		Vector3 worldExtents = Camera.main.ScreenToWorldPoint(screenExtents);

		return new Rect(worldOrigin.x, worldOrigin.y, worldExtents.x - worldOrigin.x, worldExtents.y - worldOrigin.y);
	}

	public static Vector3 GetScreenCenterInWorldPoints() {
		Rect worldRect = Manager.GetScreenRectInWorldPoints();
		return new Vector3(worldRect.x + worldRect.width / 2f, worldRect.y + worldRect.height / 2f, 0);
	}

	public static int CharToInt(char c) {
		if (!char.IsDigit(c)) throw new UnityException("char is not a digit!");

		return Convert.ToInt32(c) - 48;
	}

	public static char IntToChar(int i) {
		if (i > 9 || i < 0) throw new UnityException("not set up for multi-digit or negative ints");
		
		return Convert.ToChar(i + 48);
	}
}
