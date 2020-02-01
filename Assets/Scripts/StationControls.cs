using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationControls : MonoBehaviour {
	public PlayerControls playerControls;

	void Start() {
		//
	}

	void Update() {
		//
	}

	public virtual void updateControls() {
		if(Input.GetKeyDown(playerControls.interact)) {
			Debug.Log("detaching from station");
			playerControls.detachStation(this);
		}
	}

}
