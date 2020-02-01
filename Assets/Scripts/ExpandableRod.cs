using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandableRod: MonoBehaviour {
	public GameObject rodBody;
	public float initialRodHeight = 2.0f;
	private float rodHeight;

	// Start is called before the first frame update
	void Start() {
		setRodHeight(initialRodHeight);
	}

	// Update is called once per frame
	void Update() {
		if(Input.GetKey(KeyCode.T)) {
			float newRodHeight = rodHeight + 0.05f;
			setRodHeight(newRodHeight);
		} else if(Input.GetKey(KeyCode.G)) {
			float newRodHeight = rodHeight - 0.05f;
			if(newRodHeight < 0.2f) {
				newRodHeight = 0.2f;
			}
			setRodHeight(newRodHeight);
		}
	}

	void setRodHeight(float height) {
		var spriteRenderer = rodBody.GetComponent<SpriteRenderer>();
		var spriteSize = spriteRenderer.size;
		spriteSize.y = height;
		spriteRenderer.size = spriteSize;
		rodBody.transform.localPosition = new Vector2(0, -height);
		rodHeight = height;
	}
}
