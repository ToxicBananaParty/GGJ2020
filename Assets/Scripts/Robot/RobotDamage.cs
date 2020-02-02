using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RobotDamage: MonoBehaviour {
	public GameObject damageVisualizer;

	private bool[] damageCover = null;
	private int damageCoverWidth;
	private int damageCoverHeight;
	private Texture2D damageTexture;
	private List<MoldableShape> coveringMolds = new List<MoldableShape>();

	// Use this for initialization
	void Start() {
		generateDamageCover();
	}

	// Update is called once per frame
	void Update() {
		//
	}

	private bool checkCompletelyCovered() {
		foreach(bool b in damageCover) {
			if(b) {
				return false;
			}
		}
		return true;
	}

	private void generateDamageCover() {
		float density = 5.0f;
		var spriteRenderer = GetComponent<SpriteRenderer>();
		Debug.Log("spriteRenderer bounds size " + spriteRenderer.size);
		var bounds = spriteRenderer.bounds;
		var texture = spriteRenderer.sprite.texture;

		int width = (int)(bounds.size.x * density);
		int height = (int)(bounds.size.y * density);
		int length = width * height;

		Debug.Log("creating damage texture sized " + width + ", " + height);
		damageCover = new bool[length];
		damageTexture = new Texture2D(width,height);
		damageCoverWidth = width;
		damageCoverHeight = height;
		float xRatio = ((float)texture.width / (float)width);
		float yRatio = ((float)texture.height / (float)height);
		int damageIndex = 0;
		for(int y=0; y<height; y++) {
			for(int x=0; x<width; x++) {
				int textureX = (int)((float)x * xRatio);
				int textureY = (int)((float)y * yRatio);
				var color = texture.GetPixel(textureX, textureY);
				if(color.a > 0) {
					damageCover[damageIndex] = true;
					damageTexture.SetPixel(x, y, Color.red);
				}
				damageIndex += 1;
			}
		}

		damageTexture.Apply();
		if(damageVisualizer != null) {
			var visualSpriteRenderer = damageVisualizer.GetComponent<SpriteRenderer>();
			visualSpriteRenderer.sprite = Sprite.Create(damageTexture, new Rect(0, 0, damageCoverWidth, damageCoverHeight), new Vector2(0.5f, 0.5f));
		}
	}

	private void coverDamage(MoldableShape shape) {
		var spriteRenderer = GetComponent<SpriteRenderer>();
		var bounds = spriteRenderer.bounds;
		var rect = new Rect(bounds.min.x, bounds.min.y, bounds.size.x, bounds.size.y);

		var shapeSpriteRenderer = shape.GetComponent<SpriteRenderer>();
		var shapeBounds = shapeSpriteRenderer.bounds;
		var shapeRect = new Rect(shapeBounds.min.x, shapeBounds.min.y, shapeBounds.size.x, shapeBounds.size.y);
		if (!rect.Overlaps(shapeRect)) {
			return;
		}

		var coverRatioX = (float)damageCoverWidth / rect.width;
		var coverRatioY = (float)damageCoverHeight / rect.height;

		var matrix = new TransformMatrix();
		matrix.scale(transform.localScale.x, transform.localScale.y);
		matrix.rotate(transform.rotation.z);

		var shapeSpriteSize = shapeSpriteRenderer.sprite.bounds.size;
		var shapeTexture = shapeSpriteRenderer.sprite.texture;
		var shapePixels = shapeTexture.GetPixels();

		float shapeSpriteRatioX = shapeSpriteSize.x / (float)shapeTexture.width;
		float shapeSpriteRatioY = shapeSpriteSize.y / (float)shapeTexture.height;
		float shapeTextureCenterX = (float)shapeTexture.width / 2.0f;
		float shapeTextureCenterY = (float)shapeTexture.height / 2.0f;

		int i = 0;
		for(int y=0; y<shapeTexture.height; y++) {
			for(int x=0; x<shapeTexture.width; x++) {
				var color = shapePixels[i];
				if(color.a == 0) {
					i++;
					continue;
				}
				var centerOffsetX = (shapeTextureCenterX - (float)x) * shapeSpriteRatioX;
				var centerOffsetY = (shapeTextureCenterY - (float)y) * shapeSpriteRatioY;
				var point = matrix.transform(centerOffsetX, centerOffsetY);
				if(!rect.Contains(point)) {
					i++;
					continue;
				}
				int coverX = (int)((point.x - rect.xMin) * coverRatioX);
				int coverY = (int)((point.y - rect.yMin) * coverRatioY);
				int coverIndex = Utils.indexForPoint(coverX, coverY, damageCoverWidth);
				damageCover[coverIndex] = false;
				damageTexture.SetPixel(coverX, coverY, Color.green);

				i++;
			}
		}

		damageTexture.Apply();
		if (damageVisualizer != null) {
			var visualSpriteRenderer = damageVisualizer.GetComponent<SpriteRenderer>();
			visualSpriteRenderer.sprite = Sprite.Create(damageTexture, new Rect(0, 0, damageCoverWidth, damageCoverHeight), new Vector2(0.5f, 0.5f));
		}
	}
}
