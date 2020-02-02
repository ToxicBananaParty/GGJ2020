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
					for (int x=(int)insetAmount; x<fillEndX; x++) {
						for(int y=0; y<(int)fillHeight; y++) {
							int i = (y * width) + x;
							pixels[i] = Color.black;
						}
					}
					
					points = new Vector2[4];
					float xExtrude = (shapeWidth / 2.0f) / pixelsPerUnit;
					float yTop = -(((float)height / 2.0f) - fillHeight) / pixelsPerUnit;
					float yBottom = -((float)height / 2.0f) / pixelsPerUnit;
					points[0] = new Vector2(-xExtrude, yTop);
					points[1] = new Vector2(xExtrude, yTop);
					points[2] = new Vector2(xExtrude, yBottom);
					points[3] = new Vector2(-xExtrude, yBottom);
				}
				break;
			case MoldableShapeType.CIRCLE: {
					float radius = (float)Math.Sqrt((double)fillAmount / Math.PI);
					if(radius > circleMaxRadius) {
						radius = circleMaxRadius;
					}
					int cx = (width / 2);
					int cy = (height / 2);
					int radiusInt = (int)radius;
					for(int x=0; x<radiusInt; x++) {
						int d = (int)Mathf.Ceil(Mathf.Sqrt(radiusInt * radiusInt - x * x));
						for (int y=0; y<d; y++) {
							int px = cx + x;
							int nx = cx - x;
							int py = cy + y;
							int ny = cy - y;

							pixels[indexForPoint(px, py, width)] = Color.black;
							pixels[indexForPoint(nx, py, width)] = Color.black;
							pixels[indexForPoint(px, ny, width)] = Color.black;
							pixels[indexForPoint(nx, ny, width)] = Color.black;
						}
					}

					points = new Vector2[4];
					float halfRadius = (radius / 2.0f) / pixelsPerUnit;
					points[0] = new Vector2(-halfRadius, -halfRadius);
					points[1] = new Vector2(halfRadius, -halfRadius);
					points[2] = new Vector2(halfRadius, halfRadius);
					points[3] = new Vector2(-halfRadius, halfRadius);
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
