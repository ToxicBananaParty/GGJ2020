﻿using UnityEngine;
using System.Collections;

public class ScrapButtonInteraction: Interactable {
	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {

	}

	public override void performAction(Interactor interactor) {
		Instantiate(scrapPrefab, GameObject.Find("ScrapSpawn").transform);
	}
}