using UnityEngine;
using System.Collections;

public class ShaperMachine: MonoBehaviour {
	public MoldableShapeType initialShapeType;

	public GameObject shapingMachineDoor;
	public ScrapMetalEater scrapEater;

	public GameObject moldedShapePrefab;

	public Sprite circleDoorSprite;
	public Sprite squareDoorSprite;

	private MoldableShape moldingShape;
	private MoldableShapeType shapeType;

	// Use this for initialization
	void Start() {
		setShapeType(initialShapeType);
	}

	// Update is called once per frame
	void Update() {
		//
	}


	public void growShape() {
		if (moldingShape == null) {
			moldingShape = Instantiate(moldedShapePrefab).GetComponent<MoldableShape>();
			var shapePosition = transform.position;
			shapePosition.z = shapingMachineDoor.transform.position.z + 0.05f;
			moldingShape.transform.position = shapePosition;
			moldingShape.shapeType = shapeType;
		}
		float scrapAmount = scrapEater.eatScrap(0.02f);
		moldingShape.feedScrap(scrapAmount);
	}

	public void setShapeType(MoldableShapeType shapeType) {
		this.shapeType = shapeType;
		var doorRenderer = shapingMachineDoor.GetComponent<SpriteRenderer>();
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
