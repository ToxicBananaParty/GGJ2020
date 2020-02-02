using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MagnetStation: MonoBehaviour {
	public MagnetArm magnetArm;

	// Use this for initialization
	void Start() {
		if (magnetArm == null) {
			magnetArm = GetComponent<MagnetArm>();
		}
	}

	// Update is called once per frame
	void Update() {
		//
	}

	public bool canStickToRobot(Robot robot) {
		foreach(var shape in magnetArm.magnet.getStuckObjects<MoldableShape>()) {
			if(robot.canCoverRobot(shape)) {
				return true;
			}
		}
		return false;
	}

	public List<DamageCoverResult> stickToRobot(Robot robot) {
		var list = new List<DamageCoverResult>();
		var shapes = magnetArm.magnet.getStuckObjects<MoldableShape>();
		foreach (var shape in shapes) {
			var result = robot.coverRobot(shape);
			list.Add(result);
		}
		return list;
	}
}
