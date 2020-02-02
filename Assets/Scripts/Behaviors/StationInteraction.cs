using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationInteraction : Interactable
{
    public GameObject scrapPrefab;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

	public override bool canInteract(Interactor interactor) {
        var player = interactor.GetComponent<Player>();
        var stationControls = GetComponent<StationControls>();
        return (player != null) && (stationControls != null) && (player.controls.stationControls == null);
	}

	public override void performAction(Interactor interactor) {
	    if (!interactor.gameObject.name.Equals("Scrap Station") && !interactor.gameObject.name.Equals("Paint Station"))
	    {
	        var player = interactor.GetComponent<Player>();
	        var stationControls = GetComponent<StationControls>();
	        player.controls.attachStation(stationControls);
	    }
	    else //Do not change controls for paint or scrap stations, just press F to get scrap / paint bot
	    {
	        Instantiate(scrapPrefab, GameObject.Find("ScrapSpawn").transform);
	    }
	}
}
