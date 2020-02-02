using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationInteraction : Interactable
{
    public StationControls stationControls;

	// Start is called before the first frame update
	void Start() {
		if(stationControls == null) {
			stationControls = GetComponent<StationControls>();
		}
	}

	// Update is called once per frame
	void Update() {
        //
	}

	public override bool canInteract(Interactor interactor) {
        var player = interactor.GetComponent<Player>();
        return (player != null) && (stationControls != null) && (player.controls.stationControls == null);
	}

	public override void performAction(Interactor interactor) {
	    var player = interactor.GetComponent<Player>();
	    player.controls.attachStation(stationControls);
	}
}
