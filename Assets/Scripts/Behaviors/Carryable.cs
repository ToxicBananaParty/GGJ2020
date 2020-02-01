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
        if (!carrying)
        {
            carrying = true;
            //transform.position = 
            //Pick up
        }
        else
        {
            carrying = false;
        }
    }
}
