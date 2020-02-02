using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scrap: MonoBehaviour {

	// Start is called before the first frame update
	void Start() {
		//
	}

	// Update is called once per frame
	void Update() {
		//
	}

	public float eatScrap(float eatAmount) {
		var scale = transform.localScale;
		float averageScale = (scale.x + scale.y) / 2.0f;
		float scrapVolume = getScrapVolume();
		float newScrapVolume = scrapVolume - eatAmount;
		if(newScrapVolume <= 0) {
			newScrapVolume = 0;
			float scrapEaten = scrapVolume - newScrapVolume;
			Destroy(this.gameObject);
			return scrapEaten;
		} else {
			float scrapEaten = scrapVolume - newScrapVolume;
			float scaleAmount = newScrapVolume / scrapVolume;
			transform.localScale = transform.localScale * scaleAmount;
			return scrapEaten;
		}
	}

	public float getScrapVolume() {
		var spriteRenderer = GetComponent<SpriteRenderer>();
		var bounds = spriteRenderer.bounds;
		return bounds.size.x * bounds.size.y;
	}
}
