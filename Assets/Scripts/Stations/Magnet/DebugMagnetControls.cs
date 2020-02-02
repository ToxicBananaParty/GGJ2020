using UnityEngine;
using System.Collections;

public class DebugMagnetControls: MonoBehaviour {
	// Use this for initialization
	void Start() {

	}

	void FixedUpdate() {
		var magnetArm = GetComponent<MagnetArm>();
		if(Input.GetKey(KeyCode.J)) {
			magnetArm.moveDown();
		}
		if(Input.GetKey(KeyCode.U)) {
			magnetArm.moveUp();
		}
		if(Input.GetKey(KeyCode.H)) {
			magnetArm.moveLeft();
		}
		if(Input.GetKey(KeyCode.K)) {
			magnetArm.moveRight();
		}
		if (Input.GetKey(KeyCode.Y)) {
			magnetArm.rotateLeft();
		}
		if (Input.GetKey(KeyCode.I)) {
			magnetArm.rotateRight();
		}
	}
}
