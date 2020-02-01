using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable: MonoBehaviour {
	public GameObject actIconPrefab;
	public Vector3 actIconOffset = new Vector3(0, 0.8f, 0);

	private GameObject actIcon;
	private List<Interactor> interactors = new List<Interactor>();

	// Start is called before the first frame update
	void Start() {
		//
	}

	// Update is called once per frame
	void Update() {
		if(actIcon != null) {
			actIcon.transform.position = transform.position + actIconOffset;
		}
	}

	public void performAction() {
		// open for implementation
	}

	public void beginInteractor(Interactor interactor) {
		interactors.Add(interactor);
		if(interactors.Count == 1) {
			setActIconVisible(true);
		}
	}

	public void endInteractor(Interactor interactor) {
		interactors.Remove(interactor);
		if (interactors.Count == 0) {
			setActIconVisible(false);
		}
	}

	private void setActIconVisible(bool visible) {
		if(actIcon == null) {
			actIcon = Instantiate(actIconPrefab) as GameObject;
		}
		actIcon.SetActive(visible);
	}
}
