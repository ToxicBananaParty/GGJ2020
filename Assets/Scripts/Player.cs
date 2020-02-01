using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Player : MonoBehaviour
{
    private ControllableObject controls;
    public float walkSpeed = 5.0f;
    private Rigidbody2D myRigidbody;
    private float falltimer;


    //VARIABLES FOR SEPARATE CONTROLS FOR EACH PLAYER

    void Start()
    {
        controls = gameObject.GetComponent<ControllableObject>();
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        controls.Movement(myRigidbody);
        transform.rotation = Quaternion.identity; //Stop player rotating
    }


    void OnTriggerStay2D(Collider2D other)
    {
        //Ladder Collision
        if (other.gameObject.CompareTag("Ladder"))
        {
            controls.climbLadder(myRigidbody);
        }
    }



    //----This stuff stops the player from falling thru platforms and ladders----
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bottom"))
        {
            gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Ladder"))
        {
            myRigidbody.velocity = Vector2.zero;
            gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        }

        if (other.gameObject.CompareTag("Platform") && myRigidbody.velocity.y < -2.0f)
        {
            gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Platform"))
        {
            gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }
}
