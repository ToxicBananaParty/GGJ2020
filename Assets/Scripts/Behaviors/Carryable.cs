using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carryable : Interactable
{
    private bool carrying = false;
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
        Debug.Log("Carrying");
        if (!carrying)
        {
            carrying = true;
            transform.position = interactor.transform.position + new Vector3(0.0f, 1.0f, 0.0f);
            //Pick up
        }
        else
        {
            carrying = false;
        }
    }
}
