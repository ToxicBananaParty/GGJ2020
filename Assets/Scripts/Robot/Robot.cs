using UnityEngine;
using System.Collections;

public class Robot: MonoBehaviour {
	public RobotBody robotBody;

	// Use this for initialization
	void Start() {
		//
	}

	// Update is called once per frame
	void Update() {

	}

	public bool canCoverRobot(MoldableShape shape) {
		return robotBody.isCoveringBody(shape);
	}
}
