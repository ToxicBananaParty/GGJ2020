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

        if (!Input.GetKey(KeyCode.S))
        {
            myRigidbody.velocity = new Vector2(moveHorizontal * walkSpeed, myRigidbody.velocity.y);
        }
        else if (transform.position.y > 0.0f && Input.GetKey(KeyCode.S))
        {
            Debug.Log("Turning on trigger!");
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, -4.0f);
        }
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
                gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            }
        }

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bottom"))
        {
            Debug.Log("Turning off trigger!");
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            myRigidbody.velocity = Vector2.zero;
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }

        if (other.gameObject.CompareTag("Platform") && myRigidbody.velocity.y < -2.0f)
        {
            Debug.Log("Turning off trigger!");
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Platform"))
        {
            Debug.Log("Turning off trigger!");
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
