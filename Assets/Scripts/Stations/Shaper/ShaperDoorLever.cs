using UnityEngine;
using System.Collections;

public class ShaperDoorLever: Interactable {
	public ShaperMachine shaperMachine;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public override bool canInteract(Interactor interactor) {
		Debug.Log("can open door " + shaperMachine.canOpenDoor());
		return shaperMachine.canOpenDoor();
	}

	public override void performAction(Interactor interactor) {
		bool doorOpen = shaperMachine.isDoorOpen();
		doorOpen = !doorOpen;
		bool changed = shaperMachine.setDoorOpen(doorOpen);
		if(changed) {
			// TODO show animation for door opening
		}
	}
}
