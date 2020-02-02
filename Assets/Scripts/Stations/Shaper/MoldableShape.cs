using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoldableShapeType {
	RECTANGLE,
	CIRCLE
}

public class MoldableShape: MonoBehaviour {
	private ShaperMachine shaperMachine;

	public float initialFillAmount = 0;
	private float fillAmount = 0;

	public float shapeCompress = 0.5f;
	public MoldableShapeType shapeType = MoldableShapeType.RECTANGLE;

	public Sprite squareSprite;
	public Sprite circleSprite;

	private float circleMaxRadius {
		get { return circleSprite.bounds.size.x / 2.0f; }
	}
	private float maxFillAmount {
		get {
			switch(shapeType) {
				case MoldableShapeType.RECTANGLE: {
						var size = squareSprite.bounds.size;
						return size.x * size.y;
					}
				case MoldableShapeType.CIRCLE: {
						var size = circleSprite.bounds.size;
						return size.x * size.y;
					}
				default:
					throw new Exception("dicks n shit");
			}
		}
	}

	private static int indexForPoint(int x, int y, int width) {
		return (y * width) + x;
	}
    
	// Start is called before the first frame update
	void Start() {
		fillAmount = initialFillAmount;
		updateShapeSprite();
	}

	// Update is called once per frame
	void Update() {
		//
	}

	public void attachToMachine(ShaperMachine machine) {
		shaperMachine = machine;
		var rigidBody = GetComponent<Rigidbody2D>();
		rigidBody.gravityScale = 0;
		var collider = GetComponent<BoxCollider2D>();
		collider.enabled = false;
	}

	public void detachFromMachine() {
		shaperMachine = null;
		var localPosition = transform.localPosition;
		localPosition.z = -4;
		transform.localPosition = localPosition;
		var rigidBody = GetComponent<Rigidbody2D>();
		rigidBody.gravityScale = 1.0f;
		var collider = GetComponent<BoxCollider2D>();
		collider.enabled = true;
	}

	void updateShapeSprite() {
		var spriteRenderer = GetComponent<SpriteRenderer>();
		switch(shapeType) {
			case MoldableShapeType.RECTANGLE: {
					spriteRenderer.sprite = squareSprite;
					var spriteSize = squareSprite.bounds.size;
					float insetAmount = (spriteSize.x / 2.0f) * shapeCompress;
					float shapeWidth = spriteSize.x - (2.0f * insetAmount);
					float fillHeight = (fillAmount / shapeWidth);
					if(fillHeight > spriteSize.y) {
						fillHeight = spriteSize.y;
					}
					float prevHeight = spriteSize.y * transform.localScale.y;
					transform.localScale = new Vector2(shapeWidth / spriteSize.x, fillHeight / spriteSize.y);
					float positionDiff = (fillHeight - prevHeight) / 2.0f;
					var position = transform.position;
					position.y += positionDiff;
					transform.position = position;
				}
				break;
			case MoldableShapeType.CIRCLE: {
					spriteRenderer.sprite = circleSprite;
					var spriteRadius = circleSprite.bounds.size / 2.0f;
					float radius = (float)Math.Sqrt((double)fillAmount / Math.PI);
					if(radius > circleMaxRadius) {
						radius = circleMaxRadius;
					}
					transform.localScale = new Vector2(radius / spriteRadius.x, radius / spriteRadius.y);
				}
				break;
			default:
				throw new Exception("butts");
		}
	}

	public bool feedScrap(float scrapAmount) {
		fillAmount += scrapAmount;
		bool full = false;
		if(fillAmount > maxFillAmount) {
			fillAmount = maxFillAmount;
			full = true;
		}
		updateShapeSprite();
		return full;
	}
}
