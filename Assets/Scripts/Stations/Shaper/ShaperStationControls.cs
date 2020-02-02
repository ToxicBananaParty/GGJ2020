using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaperStationControls: StationControls {
    public ShaperMachine shapingMachine;

    

    // Start is called before the first frame update
    void Start() {
        //
    }

    // Update is called once per frame
    void Update() {
        if(playerControls != null) {
			if(Input.GetKey(playerControls.secondaryAction)) {
                shapingMachine.growShape();
			}
		}
    }


	
}
