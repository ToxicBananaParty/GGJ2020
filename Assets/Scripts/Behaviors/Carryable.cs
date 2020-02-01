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
        if (Input.GetKeyDown(KeyCode.F) && carrying)
        {
            carrying = false;
        }
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (carrying)
        {
            transform.position = theInteractor.transform.position + new Vector3(0.0f, 1.5f, 0.0f);
            gameObject.GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        }
    }

    public override void performAction(Interactor interactor)
    {
        theInteractor = interactor;
        if (!carrying)
        {
            carrying = true;
            //Pick up
        }
        else
        {
            carrying = false;
        }
    }
}
