using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MoldableShapeType {
	TRIANGLE,
	RECTANGLE,
	CIRCLE
}

public class MoldableShape: MonoBehaviour {
	public float fillAmount = 0;
	public float shapeCompress = 0.5f;
	public MoldableShapeType shapeType = MoldableShapeType.RECTANGLE;

	public Sprite squareSprite;
	public Sprite circleSprite;

	private static float circleMaxRadius = 128.0f;
	private static float circleMaxFillAmount {
		get { return (float)(Math.PI * (double)circleMaxRadius * (double)circleMaxRadius); }
	}

	private static int indexForPoint(int x, int y, int width) {
		return (y * width) + x;
	}
    
	// Start is called before the first frame update
	void Start() {
		updateShapeSprite();
	}

	// Update is called once per frame
	void Update() {
		//
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
					var spriteSize = circleSprite.bounds.size;
					float radius = (float)Math.Sqrt((double)fillAmount / Math.PI);
					if(radius > circleMaxRadius) {
						radius = circleMaxRadius;
					}
					transform.localScale = new Vector2(radius / spriteSize.x, radius / spriteSize.y);
				}
				break;
			default:
				throw new Exception("butts");
		}
	}
}
