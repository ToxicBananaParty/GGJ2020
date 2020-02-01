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

        myRigidbody.velocity = new Vector2(moveHorizontal * walkSpeed, myRigidbody.velocity.y);
        #endregion
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //Ladder Collision
        if (other.gameObject.CompareTag("Ladder"))
        {
            //Debug.Log("Colliding!");
            if (Input.GetKey(KeyCode.W)) //Move Up ladder
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, 4.0f);
            }
            else if (Input.GetKey(KeyCode.S)) //Move down ladder
            {
                myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, -4.0f);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            myRigidbody.velocity = Vector2.zero;
        }
    }
}
