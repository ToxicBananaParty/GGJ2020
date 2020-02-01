using UnityEngine;
using System.Collections;

public class MagnetArm: MonoBehaviour {
	public ExpandableRod expandableRod;
	public GameObject magnetPivot;
	public float verticalMoveSpeed = 0.01f;
	public float horizontalMoveSpeed = 0.01f;
	public float rotationSpeed = 0.2f;

	// Use this for initialization
	void Start() {
		//
	}

	// Update is called once per frame
	void Update() {
		//
	}

	void moveDown() {
		expandableRod.expandRod(verticalMoveSpeed);
	}

	void moveUp() {
		expandableRod.expandRod(-verticalMoveSpeed);
	}

	void rotateLeft() {
		var rotation = magnetPivot.transform.localRotation;
		rotation.z = rotation.z - rotationSpeed;
		magnetPivot.transform.localRotation = rotation;
	}

	void rotateRight() {
		var rotation = magnetPivot.transform.localRotation;
		rotation.z = rotation.z + rotationSpeed;
		magnetPivot.transform.localRotation = rotation;
	}
}
