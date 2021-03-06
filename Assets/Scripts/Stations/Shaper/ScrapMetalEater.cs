﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrapMetalEater: MonoBehaviour {
	public float efficiency = 3.0f;
	private List<Scrap> scraps = new List<Scrap>();

	// Start is called before the first frame update
	void Start() {
		//
	}

	// Update is called once per frame
	void Update() {
		//
	}

	void OnTriggerEnter2D(Collider2D collision) {
		var scrap = collision.gameObject.GetComponent<Scrap>();
		if(scrap != null) {
			scraps.Add(scrap);
		}
	}

	void OnTriggerExit2D(Collider2D collision) {
		var scrap = collision.gameObject.GetComponent<Scrap>();
		if (scrap != null) {
			scraps.Remove(scrap);
		}
	}

	void OnCollisionEnter2D(Collision2D collision) {
		var player = collision.gameObject.GetComponent<Player>();
		if(player != null) {
			Physics2D.IgnoreCollision(collision.collider, collision.otherCollider);
		}
	}

	public float eatScrap(float eatAmount) {
		if(scraps.Count == 0) {
			return 0;
		}
		Scrap scrap = scraps[0];
		var scrapEaten = scrap.eatScrap(eatAmount) * efficiency;
		return scrapEaten;
	}
}
