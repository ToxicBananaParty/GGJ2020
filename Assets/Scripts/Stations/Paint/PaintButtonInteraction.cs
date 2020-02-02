using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaintButtonInteraction : Interactable
{
    // Start is called before the first frame update
    public Color currentColor;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public override void performAction(Interactor interactor)
    {
        currentColor = GameObject.Find("Paint").GetComponent<Paint>().Interact();
    }
}
