using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Interactor: MonoBehaviour {
	private List<Interactable> interactables = new List<Interactable>();

	// Use this for initialization
	void Start() {
		//
	}

	// Update is called once per frame
	void Update() {
		//
	}

	void OnTriggerEnter2D(Collider2D collision) {
		var interactable = collision.gameObject.GetComponent<Interactable>();
		if(interactable != null) {
			interactable.beginInteractor(this);
			interactables.Add(interactable);
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		var interactable = collision.gameObject.GetComponent<Interactable>();
		if (interactable != null) {
			interactable.endInteractor(this);
			interactables.Remove(interactable);
		}
	}

	public bool interact() {
		if(interactables.Count == 0) {
			return false;
		}
		var interactable = interactables[0];
		interactable.performAction();
		return true;
	}
}
