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
	public float fillAmount = 256*100;
	public float shapeCompress = 0.5f;
	public MoldableShapeType shapeType = MoldableShapeType.RECTANGLE;
    
	// Start is called before the first frame update
	void Start() {
		updateShapeTexture();
	}

	// Update is called once per frame
	void Update() {
		//
	}

	void updateShapeTexture() {
		int width = 256;
		int height = 256;
		var pixels = new Color[width*height];
		for(int i=0; i<pixels.Length; i++) {
			pixels[i] = new Color(0,0,0,0);
		}
		Vector2[] points;

		float pixelsPerUnit = 100.0f;

		switch(shapeType) {
			case MoldableShapeType.RECTANGLE: {
					float insetAmount = ((float)width / 2.0f) * shapeCompress;
					float shapeWidth = (float)width - (2.0f * insetAmount);
					float fillHeight = fillAmount > 0 ? (fillAmount / shapeWidth) : 0;
					if(fillHeight > (float)height) {
						fillHeight = (float)height;
					}
					// update sprite
					int fillEndX = width - (int)insetAmount;
					int fillStartY = height - (int)fillHeight;
					for (int x=(int)insetAmount; x<fillEndX; x++) {
						for(int y=fillStartY; y<height; y++) {
							int i = (y * width) + x;
							pixels[i] = Color.black;
						}
					}
					
					// update polygon collider
					points = new Vector2[4];
					points[0] = new Vector2(-(shapeWidth / 2.0f) / pixelsPerUnit, -(fillHeight / 2.0f) / pixelsPerUnit);
					points[1] = new Vector2(-(shapeWidth / 2.0f) / pixelsPerUnit, (fillHeight / 2.0f) / pixelsPerUnit);
					points[2] = new Vector2((shapeWidth / 2.0f) / pixelsPerUnit, (fillHeight / 2.0f) / pixelsPerUnit);
					points[3] = new Vector2((shapeWidth / 2.0f) / pixelsPerUnit, -(fillHeight / 2.0f) / pixelsPerUnit);
				}
				break;
			default:
				throw new Exception("butts");
		}

		var texture = new Texture2D(width, height);
		texture.SetPixels(pixels);
		texture.Apply();
		var spriteRenderer = GetComponent<SpriteRenderer>();
		var sprite = Sprite.Create(texture, new Rect(0, 0, (float)width, (float)height), new Vector2(0.5f, 0.5f), pixelsPerUnit);
		spriteRenderer.sprite = sprite;

		var polygonCollider = GetComponent<PolygonCollider2D>();
		if(polygonCollider != null) {
			polygonCollider.SetPath(0, points);
		}
	}
}
