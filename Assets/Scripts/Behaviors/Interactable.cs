﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable: MonoBehaviour {
	public GameObject actIconPrefab;
    public Vector3 actIconOffset;

	private GameObject actIcon;
	private List<Interactor> interactors = new List<Interactor>();

	void Update() {
		if(actIcon != null) {
		    actIcon.transform.position = transform.position + actIconOffset;
		}
	}

	public virtual bool canInteract(Interactor interactor) {
		return true;
	}

	public virtual void performAction(Interactor interactor) {
		// open for implementation
	}

	public void beginInteractor(Interactor interactor) {
		interactors.Add(interactor);
		updateInteractionState();
	}

	public void endInteractor(Interactor interactor) {
		interactors.Remove(interactor);
		updateInteractionState();
	}

	public void updateInteractionState() {
		bool hasAbleInteractor = false;
		foreach (var interactor in interactors) {
			if (canInteract(interactor)) {
				hasAbleInteractor = true;
				break;
			}
		}
		setActIconVisible(hasAbleInteractor);
	}

	private void setActIconVisible(bool visible) {
		if(actIcon == null && actIconPrefab != null) {
            actIcon = Instantiate(actIconPrefab);
		    actIcon.transform.position = transform.position + actIconOffset;
            actIcon.transform.localScale *= 5;
        }
		if(actIcon != null) {
			actIcon.SetActive(visible);
		}
	}
}
