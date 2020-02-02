using System.Collections;
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
		if(active) {
			foreach (var stuckObject in stuckObjects) {
				var magnetic = stuckObject.GetComponent<Magnetic>();
				if(magnetic != null && !magnetic.active) {
					continue;
				}
				var direction = (transform.position - stuckObject.transform.position).normalized;
				direction.z = 0;
				var force = direction * attractionStrength;
				stuckObject.GetComponent<Rigidbody2D>().AddForceAtPosition(force,transform.position);
			}
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
		if (active) {
			foreach (var stuckObject in stuckObjects) {
				var magnetic = stuckObject.GetComponent<Magnetic>();
				if(magnetic != null && !magnetic.active) {
					continue;
				}
				stuckObject.transform.position = stuckObject.transform.position + shiftAmount;
			}
		}
	}

	public List<T> getStuckObjects<T>() where T: class {
		List<T> objs = new List<T>();
		foreach(var obj in stuckObjects) {
			T typeObj = obj.GetComponent<T>();
			if (typeObj != null) {
				objs.Add(typeObj);
			}
		}
		return objs;
	}
}
