using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carryable : Interactable
{
    private bool carrying = false;
    private Interactor theInteractor;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    { 
        if (carrying)
        {
            transform.position = theInteractor.transform.position + new Vector3(0.0f, 1.25f, 0.0f);
            gameObject.GetComponent<Rigidbody2D>().mass = 0.0f; //So can still climb while carrying
            theInteractor.gameObject.GetComponent<Animator>().SetBool("carrying", true);
        }
    }

    public override void performAction(Interactor interactor)
    {
        theInteractor = interactor;
        if (!carrying)
        {
            carrying = true;
        }
        else
        {
            carrying = false;
            theInteractor.gameObject.GetComponent<Player>().climbSpeed = 0.1f;
            gameObject.GetComponent<Rigidbody2D>().mass = 1.0f;
            theInteractor.gameObject.GetComponent<Animator>().SetBool("carrying", false);
        }
    }
}
