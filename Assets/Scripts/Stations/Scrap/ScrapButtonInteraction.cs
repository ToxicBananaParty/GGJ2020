using UnityEngine;
using System.Collections;

public class ScrapButtonInteraction: Interactable {
	public GameObject scrapPrefab;
	// Use this for initialization
	void Start() {

	}

	// Update is called once per frame
	void Update() {
		//
	}

	public override void performAction(Interactor interactor) {
		Instantiate(scrapPrefab, new Vector3(22.0f, 11.0f, 0.0f), Quaternion.identity);
	    GameObject.Find("Main Camera").GetComponent<GameManager>().cash -= 10.0f;
        interactor.gameObject.GetComponent<Animator>().SetTrigger("Press");
    }
}
