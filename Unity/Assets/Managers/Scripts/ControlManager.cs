using UnityEngine;
using System.Collections;
using InControl;

public class ControlManager : MonoBehaviour {
	public float joystickDeadzoneSize = 0.5f;

	public KeyCode jumpKey = KeyCode.Space;
	public KeyCode shootKey = KeyCode.F;

	public InputControlType jumpButton = InputControlType.Action1;
	public InputControlType shootButton = InputControlType.Action3;

	public bool GetShoot(ControlState state) {
		if (state == ControlState.WasPressed) {
			return Input.GetKeyDown(shootKey) || InputManager.ActiveDevice.GetControl(shootButton).WasPressed || Input.GetMouseButtonDown(0);
		}
		else if (state == ControlState.IsPressed) {
			return Input.GetKey(shootKey) || InputManager.ActiveDevice.GetControl(shootButton).IsPressed || Input.GetMouseButton(0);
		}
		else if (state == ControlState.WasReleased) {
			return Input.GetKeyUp(shootKey) || InputManager.ActiveDevice.GetControl(shootButton).WasReleased || Input.GetMouseButtonUp(0);
		}

		return false;
	}

	public bool GetJump(ControlState state) {
		if (state == ControlState.WasPressed) {
			return Input.GetKeyDown(jumpKey) || InputManager.ActiveDevice.GetControl(jumpButton).WasPressed ;
		}
		else if (state == ControlState.IsPressed) {
			return Input.GetKey(jumpKey) || InputManager.ActiveDevice.GetControl(jumpButton).IsPressed;
		}
		else if (state == ControlState.WasReleased) {
			return Input.GetKeyUp(jumpKey) || InputManager.ActiveDevice.GetControl(jumpButton).WasReleased;
		}
		
		return false;
	}

	public bool GetRight(ControlState state) {
		if (state == ControlState.WasPressed) {
			bool joystickWasPressed = InputManager.ActiveDevice.Analogs[0].WasPressed && InputManager.ActiveDevice.Direction.x > joystickDeadzoneSize;
			return joystickWasPressed || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D);
		}
		else if (state == ControlState.IsPressed) {
			bool joystickIsPressed = InputManager.ActiveDevice.Analogs[0].IsPressed && InputManager.ActiveDevice.Direction.x > joystickDeadzoneSize;
			return joystickIsPressed || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D);
		}
		else if (state == ControlState.WasReleased) {
			bool joystickWasReleased = InputManager.ActiveDevice.Analogs[0].WasReleased;
			return joystickWasReleased || Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.D);
		}

		return false;
	}

	public bool GetLeft(ControlState state) {
		if (state == ControlState.WasPressed) {
			bool joystickWasPressed = InputManager.ActiveDevice.Analogs[0].WasPressed && InputManager.ActiveDevice.Direction.x < -joystickDeadzoneSize;
			return joystickWasPressed || Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A);
		}
		else if (state == ControlState.IsPressed) {
			bool joystickIsPressed = InputManager.ActiveDevice.Analogs[0].IsPressed && InputManager.ActiveDevice.Direction.x < -joystickDeadzoneSize;
			return joystickIsPressed || Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A);
		}
		else if (state == ControlState.WasReleased) {
			bool joystickWasReleased = InputManager.ActiveDevice.Analogs[0].WasReleased;
			return joystickWasReleased || Input.GetKeyUp(KeyCode.LeftArrow) || Input.GetKeyUp(KeyCode.A);
		}

		return false;
	}

	public bool GetDown(ControlState state) {
		if (state == ControlState.WasPressed) {
			bool joystickWasPressed = InputManager.ActiveDevice.Analogs[0].WasPressed && InputManager.ActiveDevice.Direction.y < -joystickDeadzoneSize;
			return joystickWasPressed || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.S);
		}
		else if (state == ControlState.IsPressed) {
			bool joystickIsPressed = InputManager.ActiveDevice.Analogs[0].IsPressed && InputManager.ActiveDevice.Direction.x < -joystickDeadzoneSize;
			return joystickIsPressed || Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S);
		}
		else if (state == ControlState.WasReleased) {
			bool joystickWasReleased = InputManager.ActiveDevice.Analogs[0].WasReleased;
			return joystickWasReleased || Input.GetKeyUp(KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.S);
		}

		return false;
	}

	public bool GetUp(ControlState state) {
		if (state == ControlState.WasPressed) {
			bool joystickWasPressed = InputManager.ActiveDevice.Analogs[0].WasPressed && InputManager.ActiveDevice.Direction.y > joystickDeadzoneSize;
			return joystickWasPressed || Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W);
		}
		else if (state == ControlState.IsPressed) {
			bool joystickIsPressed = InputManager.ActiveDevice.Analogs[0].IsPressed && InputManager.ActiveDevice.Direction.y > joystickDeadzoneSize;
			return joystickIsPressed || Input.GetKey(KeyCode.UpArrow) || Input.GetKey(KeyCode.W);
		}
		else if (state == ControlState.WasReleased) {
			bool joystickWasReleased = InputManager.ActiveDevice.Analogs[0].WasReleased;
			return joystickWasReleased || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.W);
		}

		return false;
	}
}
