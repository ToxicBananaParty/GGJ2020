using UnityEngine;
using System.Collections;

public class ShaperMachineDoor: MonoBehaviour {
	public GameObject doorBody;

	public Sprite circleDoorSprite;
	public Sprite squareDoorSprite;

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
}
