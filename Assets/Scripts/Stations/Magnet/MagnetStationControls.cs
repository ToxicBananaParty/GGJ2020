using UnityEngine;
using System.Collections;

public class MagnetStationControls: StationControls {
	public MagnetArm magnetArm;

	// Use this for initialization
	void Start() {
		if(magnetArm == null) {
			magnetArm = GetComponent<MagnetArm>();
		}
	}

	void FixedUpdate() {
		if(playerControls != null) {
			if (Input.GetKey(playerControls.left)) {
				magnetArm.moveLeft();
			}
			if (Input.GetKey(playerControls.right)) {
				magnetArm.moveRight();
			}
			if (Input.GetKey(playerControls.up)) {
				magnetArm.moveUp();
			}
			if (Input.GetKey(playerControls.down)) {
				magnetArm.moveDown();
			}
		}
	}

	void Update() {
		if(playerControls != null) {
			//Toggle magnetism
			if (Input.GetKeyDown(playerControls.secondaryAction)) {
				magnetArm.magnet.active = !magnetArm.magnet.active;
			}
		}
	}
}
