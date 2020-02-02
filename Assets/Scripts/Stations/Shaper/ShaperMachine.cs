using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShaperMachine: MonoBehaviour {
	public MoldableShapeType initialShapeType;

	public ShaperMachineDoor shaperMachineDoor;
	public ScrapMetalEater scrapEater;

	public GameObject moldedShapePrefab;

	public MoldableShape moldingShape;
	private MoldableShapeType shapeType;

	public Sprite circleMachineSprite;
	public Sprite squareMachineSprite;

	private List<MoldableShape> touchingShapes = new List<MoldableShape>();

	// Use this for initialization
	void Start() {
		setShapeType(initialShapeType);
	}

	// Update is called once per frame
	void Update() {
		//
	}

	void OnTriggerEnter2D(Collider2D collision) {
		var shape = collision.gameObject.GetComponent<MoldableShape>();
		if(shape != null) {
			touchingShapes.Add(shape);
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		var shape = collision.gameObject.GetComponent<MoldableShape>();
		if (shape != null) {
			touchingShapes.Remove(shape);
		}
	}



	public void growShape() {
		if (moldingShape == null) {
			moldingShape = Instantiate(moldedShapePrefab).GetComponent<MoldableShape>();
			var shapePosition = transform.position;
			shapePosition.z = shaperMachineDoor.transform.position.z + 0.05f;
			moldingShape.transform.position = shapePosition;
			moldingShape.attachToMachine(this);
			moldingShape.shapeType = shapeType;

		}
		float scrapAmount = scrapEater.eatScrap(0.02f);
		moldingShape.feedScrap(scrapAmount);
	}

	public bool isGrowingShape() {
		return (moldingShape != null);
	}

	public void setShapeType(MoldableShapeType shapeType) {
		if(moldingShape != null) {
			throw new System.Exception("can't change shape type while growing a shape");
		}
		this.shapeType = shapeType;
		shaperMachineDoor.setShapeType(shapeType);
		var doorRenderer = GetComponent<SpriteRenderer>();
		switch (shapeType) {
			case MoldableShapeType.RECTANGLE:
				doorRenderer.sprite = squareMachineSprite;
				break;
			case MoldableShapeType.CIRCLE:
				doorRenderer.sprite = circleMachineSprite;
				break;
		}
	}

	public void cycleShape() {
		if(isGrowingShape()) {
			return;
		}
		MoldableShapeType newShapeType;
		switch(shapeType) {
			case MoldableShapeType.RECTANGLE:
				newShapeType = MoldableShapeType.CIRCLE;
				break;
			case MoldableShapeType.CIRCLE:
				newShapeType = MoldableShapeType.RECTANGLE;
				break;
			default:
				throw new System.Exception("fuk");
		}
		setShapeType(newShapeType);
	}

	public bool canOpenDoor() {
		bool hasNonOwningShape = false;
		foreach(var shape in touchingShapes) {
			if(shape != moldingShape) {
				hasNonOwningShape = true;
				break;
			}
		}
		return !hasNonOwningShape;
	}

	public bool setDoorOpen(bool open) {
		if(open) {
			if(!canOpenDoor()) {
				return false;
			}
			shaperMachineDoor.setDoorOpen(true);
			releaseMoldingShape();
		} else {
			shaperMachineDoor.setDoorOpen(false);
		}
		return true;
	}

	public bool isDoorOpen() {
		return shaperMachineDoor.isDoorOpen();
	}

	private void releaseMoldingShape() {
		if(moldingShape == null) {
			return;
		}
		moldingShape.detachFromMachine();
	}
}
