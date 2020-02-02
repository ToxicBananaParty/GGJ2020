using UnityEngine;
using System.Collections;

public class Globals: MonoBehaviour {
	public static Globals shared;

	// Use this for initialization
	void Start() {
		shared = this;
	}

	// Update is called once per frame
	void Update() {
		//
	}
}
