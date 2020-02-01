using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    private Rigidbody2D myRigidbody;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        #region Player Movement

        float moveHorizontal = Input.GetAxis("Horizontal");

        if (Input.GetKey(KeyCode.W)) //Move Up ladder
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 4.0f);
        }
        else if (Input.GetKey(KeyCode.S)) //Move down ladder
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, -4.0f);
        }


        myRigidbody.velocity = new Vector2(moveHorizontal * walkSpeed, myRigidbody.velocity.y);
        



        #endregion
    }
}
