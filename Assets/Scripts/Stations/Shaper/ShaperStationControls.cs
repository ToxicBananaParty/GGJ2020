using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShaperStationControls: StationControls {
    public GameObject shapingMachine;
    public GameObject shapingMachineDoor;
    public ScrapMetalEater scrapEater;
    public GameObject moldedShapePrefab;

    private MoldableShape moldingShape;

    // Start is called before the first frame update
    void Start() {
        //
    }

    // Update is called once per frame
    void Update() {
        if(playerControls != null) {
			if(Input.GetKey(playerControls.secondaryAction)) {
                growShape();
			}
		}
    }


	void growShape() {
		if(moldingShape == null) {
            moldingShape = Instantiate(moldedShapePrefab).GetComponent<MoldableShape>();
			var shapePosition = shapingMachine.transform.position;
            shapePosition.z = -4;
            moldingShape.transform.position = shapePosition;
		}
        float scrapAmount = scrapEater.eatScrap(0.02f);
        moldingShape.feedScrap(scrapAmount);
	}
}
