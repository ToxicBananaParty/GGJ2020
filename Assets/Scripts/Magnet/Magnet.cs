using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Magnet: MonoBehaviour {
	private List<GameObject> stuckObjects;

	// Start is called before the first frame update
	void Start() {
		//
	}

	// Update is called once per frame
	void Update() {
		//
	}

	void OnTriggerEnter(Collider collider) {
		if(collider.gameObject.tag.Equals("Magnetic")) {
			stuckObjects.Add(collider.gameObject);
		}
	}

	void OnTriggerExit(Collider collider) {
		stuckObjects.Remove(collider.gameObject);
	}
}
