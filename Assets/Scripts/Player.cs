using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    private Rigidbody2D myRigidbody;
    private float falltimer;
    // Start is called before the first frame update
    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Movement();
    }

    void Movement()
    {

        //Jumping
        if (Input.GetKeyDown(KeyCode.Space))
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, myRigidbody.velocity.y + 5.0f);
        }
        falltimer += Time.deltaTime * 12.5f;
        #region Player Movement
        float moveHorizontal = Input.GetAxis("Horizontal");

        if (!Input.GetKey(KeyCode.S))
        {
            falltimer = 0;
            myRigidbody.velocity = new Vector2(moveHorizontal * walkSpeed, myRigidbody.velocity.y);
        }
        else if (transform.position.y > 0.0f && Input.GetKey(KeyCode.S))
        {
            Debug.Log("Timer: " + falltimer);
            gameObject.GetComponent<BoxCollider2D>().isTrigger = true;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, -4.0f - falltimer);
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
