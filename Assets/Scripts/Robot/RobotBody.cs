using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public enum DamageCoverResult {
	EVERYTHING_COVERED,
	SOME_DAMAGE_COVERED,
	NO_DAMAGE_COVERED,
	NOT_COVERED
}

public class RobotBody: MonoBehaviour {
	public GameObject damagePrefab;

	private List<RobotDamage> damages = new List<RobotDamage>();
	private List<MoldableShape> touchingShapes = new List<MoldableShape>();

	// Use this for initialization
	void Start() {
		randomizeDamage(1, 6);
	}

	// Update is called once per frame
	void Update() {
		//
	}

	public List<MoldableShape> getTouchingShapes() {
		return touchingShapes;
	}

	public DamageCoverResult coverDamage(MoldableShape shape) {
		var hasShape = false;
		foreach(var cmpShape in touchingShapes) {
			if(shape == cmpShape) {
				hasShape = true;
				break;
			}
		}
		if(!hasShape) {
			return DamageCoverResult.NOT_COVERED;
		}
		var coveringDamages = shape.getTouchingDamages();
		foreach(var damage in coveringDamages) {
			damage.coverDamage(shape);
		}
		var rigidBody = shape.GetComponent<Rigidbody2D>();
		rigidBody.gravityScale = 0.0f;
		var collider = shape.GetComponent<BoxCollider2D>();
		collider.enabled = false;
		if(coveringDamages.Count == 0) {
			return DamageCoverResult.NO_DAMAGE_COVERED;
		}
		var covered = true;
		foreach(var damage in damages) {
			if(!damage.isCovered()) {
				covered = false;
				break;
			}
		}
		if(covered) {
			return DamageCoverResult.EVERYTHING_COVERED;
		}
		return DamageCoverResult.SOME_DAMAGE_COVERED;
	}

	public void randomizeDamage(int minDamage, int maxDamage) {
		var spriteRenderer = GetComponent<SpriteRenderer>();
		var bounds = spriteRenderer.bounds;
		int numDamage = (int)Random.Range(minDamage, (maxDamage + 1));
		List<RobotDamage> newDamages = new List<RobotDamage>();
		for(int i=0; i<numDamage; i++) {
			var damage = Instantiate(damagePrefab).GetComponent<RobotDamage>();

			var scale = Random.Range(0.1f, 0.6f);
			damage.transform.localScale = new Vector3(scale, scale, scale);

			/*var rotation = Random.Range(-180.0f, 180.0f);
			var localRotation = damage.transform.localRotation;
			localRotation.z = rotation;
			damage.transform.localRotation = localRotation;*/

			var damageSpriteRenderer = damage.GetComponent<SpriteRenderer>();
			var damageBounds = damageSpriteRenderer.bounds;
			var damagePadding = damageBounds.size / 2.0f;
			damage.transform.position = new Vector3(
				Random.Range(bounds.min.x + damagePadding.x, bounds.max.x - damagePadding.x),
				Random.Range(bounds.min.y + damagePadding.y, bounds.max.y - damagePadding.y),
				damage.transform.position.z);

			damage.transform.SetParent(this.transform);
			newDamages.Add(damage);
		}

		//applyDamages(newDamages);
	}

	public void applyDamages(List<RobotDamage> newDamages) {
		var spriteRenderer = GetComponent<SpriteRenderer>();
		var bounds = spriteRenderer.bounds;
		var currentTexture = spriteRenderer.sprite.texture;

		var prevRenderTexture = RenderTexture.active;
		var renderTexture = new RenderTexture(currentTexture.width, currentTexture.height, 0);
		renderTexture.Create();
		var newTexture = new Texture2D(currentTexture.width, currentTexture.height);
		RenderTexture.active = renderTexture;

		Graphics.Blit(currentTexture, renderTexture);

		float widthRatio = (float)currentTexture.width / bounds.size.x;
		float heightRatio = (float)currentTexture.height / bounds.size.y;
		float width = (float)newTexture.width;
		float height = (float)newTexture.height;
		foreach(var damage in newDamages) {
			GL.PushMatrix();
			GL.LoadPixelMatrix(0, currentTexture.width, currentTexture.height, 0);

			var damageSpriteRenderer = damage.GetComponent<SpriteRenderer>();
			var damageBounds = damageSpriteRenderer.bounds;
			var damageTexture = damageSpriteRenderer.sprite.texture;
			var rect = new Rect(
				(damageBounds.min.x - bounds.min.x) * widthRatio,
				(bounds.max.y - damageBounds.max.y) * heightRatio,
				damageBounds.size.x * widthRatio,
				damageBounds.size.y * heightRatio);
			Graphics.DrawTexture(rect, damageTexture);
			damages.Add(damage);

			var color = Color.red;
			color.a = 0.4f;
			damageSpriteRenderer.color = color;

			GL.PopMatrix();
		}

		newTexture.ReadPixels(new Rect(0, 0, renderTexture.width, renderTexture.height), 0, 0);
		newTexture.Apply();

		RenderTexture.active = prevRenderTexture;
		renderTexture.Release();

		spriteRenderer.sprite = Sprite.Create(newTexture, new Rect(0, 0, (float)currentTexture.width, (float)currentTexture.height), new Vector2(0.5f, 0.5f));
	}
}
