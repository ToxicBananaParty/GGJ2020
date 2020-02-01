using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float walkSpeed = 5.0f;
    private Rigidbody2D myRigidbody;
    private float falltimer;


    //VARIABLES FOR SEPARATE CONTROLS FOR EACH PLAYER
    private KeyCode left, right, up, down, jump;

    void Start()
    {
        myRigidbody = gameObject.GetComponent<Rigidbody2D>();
        Debug.Log(GameObject.Find("Players").transform.childCount);
        
    }

    void FixedUpdate()
    {
        left = KeyCode.A;
        right = KeyCode.D;
        up = KeyCode.W;
        down = KeyCode.S;
        jump = KeyCode.Space;
        Movement();
        transform.rotation = Quaternion.identity; //Stop player rotating
    }

    private float moveHorizontal;
    void Movement()
    {

        //Jumping
        if (Input.GetKeyDown(jump))
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, myRigidbody.velocity.y + 5.0f);
        }
        falltimer += Time.deltaTime * 12.5f;
        #region Player Movement

        //TODO: Make players slow down/stop moving when not pressing any buttons
        if (Input.GetKey(left))
        {
            moveHorizontal = -1.5f;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Input.GetKey(right))
        {
            moveHorizontal = 1.5f;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (!Input.GetKey(down))
        {
            falltimer = 0;
            myRigidbody.velocity = new Vector2(moveHorizontal * walkSpeed, myRigidbody.velocity.y);
            transform.rotation = Quaternion.identity;
        }
        else if (transform.position.y > 0.0f && Input.GetKey(KeyCode.S))
        {
            //Debug.Log("Timer: " + falltimer);
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, -4.0f - falltimer);
            transform.rotation = Quaternion.identity;
        }

        if(!Input.GetKey(down) && !Input.GetKey(right) && !Input.GetKey(left))
        {
            myRigidbody.velocity = new Vector2(0.0f, myRigidbody.velocity.y);
            transform.rotation = Quaternion.identity;
        }
        #endregion

        
    }

    void OnTriggerStay2D(Collider2D other)
    {
        //Ladder Collision
        if (other.gameObject.CompareTag("Ladder"))
        {
            //Debug.Log("Colliding!");
            if (Input.GetKey(up)) //Move Up ladder
            {
                myRigidbody.velocity = new Vector2(0.0f, 4.0f);
                gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
                transform.rotation = Quaternion.identity;
            }
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Bottom"))
        {
            //Debug.Log("Turning off trigger!");
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
            //Debug.Log("Turning off trigger!");
            gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Platform"))
        {
            //Debug.Log("Turning off trigger!");
            gameObject.GetComponent<CircleCollider2D>().isTrigger = false;
        }
    }
}
