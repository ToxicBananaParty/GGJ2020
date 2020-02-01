using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StationInteraction : Interactable
{
    private bool attached = false;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void performAction(Interactor interactor)
    {

        if (!attached) {
            foreach (Transform child in GameObject.Find("Players").transform)
            {
                child.gameObject.GetComponent<PlayerControls>().stationControls =
                    gameObject.GetComponent<StationControls>();
                child.gameObject.GetComponent<Rigidbody2D>().Sleep();
            }

            attached = true;
        }
        else
        {
            foreach (Transform child in GameObject.Find("Players").transform)
            {
                child.gameObject.GetComponent<PlayerControls>().stationControls = null;
                child.gameObject.GetComponent<Rigidbody2D>().WakeUp();
            }
            attached = false;
        }
    }
}
