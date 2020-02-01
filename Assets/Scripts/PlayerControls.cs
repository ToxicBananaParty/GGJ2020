using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public StationControls stationControls;
    public KeyCode up, down, left, right, jump;

    private Interactor interactor;
    void Start()
    {
        left = KeyCode.A;
        right = KeyCode.D;
        up = KeyCode.W;
        down = KeyCode.S;
        jump = KeyCode.Space;
        stationControls = null;
        interactor = GetComponent<Interactor>();
    }

    void FixedUpdate()
    {
        if (stationControls == null)
        {
            Movement(gameObject.GetComponent<Rigidbody2D>());
        }
        else
        {
            //Do station controls
        }
    }



    private float moveHorizontal, falltimer;
    public void Movement(Rigidbody2D myRigidbody)
    {
        if (Input.GetKeyDown(jump))
        {
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, myRigidbody.velocity.y + 5.0f);
        }
        falltimer += Time.deltaTime * 12.5f;
        if (Input.GetKey(left))
        {
            moveHorizontal = -2.0f;
            gameObject.GetComponent<SpriteRenderer>().flipX = false;
        }

        if (Input.GetKey(right))
        {
            moveHorizontal = 2.0f;
            gameObject.GetComponent<SpriteRenderer>().flipX = true;
        }

        if (!Input.GetKey(down))
        {
            falltimer = 0;
            myRigidbody.velocity = new Vector2(moveHorizontal * 5.0f, myRigidbody.velocity.y);
            transform.rotation = Quaternion.identity;
        }
        else if (transform.position.y > 0.0f && Input.GetKey(KeyCode.S))
        {
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            myRigidbody.velocity = new Vector2(myRigidbody.velocity.x, -4.0f - falltimer);
            transform.rotation = Quaternion.identity;
        }

        if (!Input.GetKey(down) && !Input.GetKey(right) && !Input.GetKey(left))
        {
            myRigidbody.velocity = new Vector2(0.0f, myRigidbody.velocity.y);
            transform.rotation = Quaternion.identity;
        }

        if (Input.GetKeyDown(KeyCode.F))
        {
            interactor.interact();
        }
    }

    public void climbLadder(Rigidbody2D myRigidbody)
    {
        //Debug.Log("Colliding!");
        if (Input.GetKey(up)) //Move Up ladder
        {
            myRigidbody.velocity = new Vector2(0.0f, 7.0f);
            gameObject.GetComponent<CircleCollider2D>().isTrigger = true;
            transform.rotation = Quaternion.identity;
        }
    }
}
