using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Magnetic: MonoBehaviour {
	private List<Magnet> magnets = new List<Magnet>();
	public bool active = true;

	// Use this for initialization
	void Start() {
		//
	}

	// Update is called once per frame
	void Update() {

	}

	public void enterMagnetField(Magnet magnet) {
		magnets.Add(magnet);
	}

	public void leaveMagnetField(Magnet magnet) {
		magnets.Remove(magnet);
	}

	public bool isStuckToMagnet() {
		if(!active) {
			return false;
		}
		var hasActiveMagnet = false;
		foreach (var magnet in magnets) {
			if(magnet.active) {
				hasActiveMagnet = true;
				break;
			}
		}
		return hasActiveMagnet;
	}
}
