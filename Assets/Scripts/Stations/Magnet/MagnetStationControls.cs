using UnityEngine;
using System.Collections;

public class MagnetStationControls: StationControls {
	public MagnetStation magnetStation;

	// Use this for initialization
	void Start() {
		if(magnetStation == null) {
			magnetStation = GetComponent<MagnetStation>();
		}
	}

	void FixedUpdate() {
		if(playerControls != null) {
			if (Input.GetKey(playerControls.left)) {
				magnetStation.magnetArm.moveLeft();
			}
			if (Input.GetKey(playerControls.right)) {
				magnetStation.magnetArm.moveRight();
			}
			if (Input.GetKey(playerControls.up)) {
				magnetStation.magnetArm.moveUp();
			}
			if (Input.GetKey(playerControls.down)) {
				magnetStation.magnetArm.moveDown();
			}
		}
	}

	void Update() {
		if(playerControls != null) {
			//Toggle magnetism
			if (Input.GetKeyDown(playerControls.secondaryAction)) {
				magnetStation.magnetArm.magnet.active = !magnetStation.magnetArm.magnet.active;
			}
			var robot = Globals.shared.currentRobot;
			if (robot != null && Input.GetKeyDown(playerControls.tertiaryAction)) {
				var results = magnetStation.stickToRobot(robot);
				foreach(var result in results) {
					Debug.Log("stick result " + result);
				}
			}
		}
	}
}
