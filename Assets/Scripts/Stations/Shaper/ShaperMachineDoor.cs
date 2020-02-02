using UnityEngine;
using System.Collections;

public class ShaperMachineDoor: MonoBehaviour {
	public GameObject doorBody;

	public Sprite circleDoorSprite;
	public Sprite squareDoorSprite;

	private bool doorOpen = false;

	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public void setShapeType(MoldableShapeType shapeType) {
		var doorRenderer = doorBody.GetComponent<SpriteRenderer>();
		switch (shapeType) {
			case MoldableShapeType.RECTANGLE:
				doorRenderer.sprite = squareDoorSprite;
				break;
			case MoldableShapeType.CIRCLE:
				doorRenderer.sprite = circleDoorSprite;
				break;
		}
	}

	public void setDoorOpen(bool open) {
		if(open) {
			doorBody.transform.localPosition = new Vector3(0, 3.0f, 0);
		} else {
			doorBody.transform.localPosition = Vector3.zero;
		}
		doorOpen = open;
	}

	public bool isDoorOpen() {
		return doorOpen;
	}
}
