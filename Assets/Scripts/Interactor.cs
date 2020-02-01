using UnityEngine;
using System.Collections;

public class Interactor: MonoBehaviour {
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
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		var interactable = collision.gameObject.GetComponent<Interactable>();
		if (interactable != null) {
			interactable.endInteractor(this);
		}
	}
}
