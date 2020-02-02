﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet: MonoBehaviour {
	private List<GameObject> stuckObjects = new List<GameObject>();
	public float attractionStrength = 200.0f;
	public bool active = true;

	// Start is called before the first frame update
	void Start() {
		//
	}

	void FixedUpdate() {
		foreach (var stuckObject in stuckObjects) {
			var direction = (transform.position - stuckObject.transform.position).normalized;
			direction.z = 0;
			var force = direction * attractionStrength;
			stuckObject.GetComponent<Rigidbody2D>().AddRelativeForce(force);
		}
	}

	void OnTriggerEnter2D(Collider2D collision) {
		var magnetic = collision.gameObject.GetComponent<Magnetic>();
		if (collision.gameObject.CompareTag("Magnetic") || magnetic != null) {
			stuckObjects.Add(collision.gameObject);
			if(magnetic != null) {
				magnetic.enterMagnetField(this);
			}
			Debug.Log("magnet stuck object");
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		if(stuckObjects.Contains(collision.gameObject)) {
			var magnetic = collision.gameObject.GetComponent<Magnetic>();
			stuckObjects.Remove(collision.gameObject);
			if(magnetic != null) {
				magnetic.leaveMagnetField(this);
			}
			Debug.Log("magnet unstuck object");
		}
	}

	public void shiftStuckObjects(Vector3 shiftAmount) {
		foreach(var stuckObject in stuckObjects) {
			stuckObject.transform.position = stuckObject.transform.position + shiftAmount;
		}
	}
}