using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExpandableRod: MonoBehaviour {
	public GameObject rodBody;
	public GameObject rodAttachment;
	public float initialRodLength = 2.0f;
	public float minRodLength = 0.5f;
	public float maxRodLength = 10.0f;
	private float rodLength;

	// Start is called before the first frame update
	void Start() {
		setRodLength(initialRodLength);
	}

	// Update is called once per frame
	void Update() {
		//
	}

	public void setRodLength(float length) {
		var spriteRenderer = rodBody.GetComponent<SpriteRenderer>();
		var spriteSize = spriteRenderer.size;
		spriteSize.y = length;
		spriteRenderer.size = spriteSize;
		rodBody.transform.localPosition = new Vector2(0, -length);
		if(rodAttachment != null) {
			rodAttachment.transform.position = transform.position + new Vector3(0, -length, 0);
		}
		rodLength = length;
	}

	public float expandRod(float amount) {
		float oldRodLength = rodLength;
		float newRodLength = rodLength + amount;
		if(newRodLength < minRodLength) {
			newRodLength = minRodLength;
		}
		else if(newRodLength > maxRodLength) {
			newRodLength = maxRodLength;
		}
		setRodLength(newRodLength);
		return rodLength - oldRodLength;
	}
}
