using UnityEngine;
using System.Collections;

public class MagnetArm: MonoBehaviour {
	public ExpandableRod expandableRod;
	public GameObject magnetPivot;
	public Magnet magnet;
	public float verticalMoveSpeed = 0.05f;
	public float horizontalMoveSpeed = 0.05f;
	public float rotationSpeed = 0.5f;

	// Use this for initialization
	void Start() {
		//
	}

	// Update is called once per frame
	void Update() {
		//
	}

	public void moveDown() {
		float moveAmount = expandableRod.expandRod(verticalMoveSpeed);
		magnet.shiftStuckObjects(new Vector3(0, moveAmount, 0));
	}

	public void moveUp() {
		float moveAmount = expandableRod.expandRod(-verticalMoveSpeed);
		magnet.shiftStuckObjects(new Vector3(0, moveAmount, 0));
	}

	public void moveLeft() {
		transform.position = transform.position + new Vector3(-horizontalMoveSpeed, 0, 0);
		magnet.shiftStuckObjects(new Vector3(-horizontalMoveSpeed/2.0f, 0, 0));
	}

	public void moveRight() {
		transform.position = transform.position + new Vector3(horizontalMoveSpeed, 0, 0);
		magnet.shiftStuckObjects(new Vector3(horizontalMoveSpeed/2.0f, 0, 0));
	}

	public void rotateLeft() {
		var rotation = magnetPivot.transform.localRotation;
		rotation.z = rotation.z - rotationSpeed;
		magnetPivot.transform.localRotation = rotation;
	}

	public void rotateRight() {
		var rotation = magnetPivot.transform.localRotation;
		rotation.z = rotation.z + rotationSpeed;
		magnetPivot.transform.localRotation = rotation;
	}
}
