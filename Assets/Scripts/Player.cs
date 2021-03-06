﻿using System.Collections;
using System.Collections.Generic;
using UnityEditor.Timeline;
using UnityEngine;

public class Player : MonoBehaviour
{
    public PlayerControls controls;
    public float walkSpeed = 0.1f;
    public float climbSpeed = 0.1f;
    public float jumpVelocity = 20.0f;
    public float gravityScale = 10.0f;
    private List<GameObject> ladders = new List<GameObject>();


    //VARIABLES FOR SEPARATE CONTROLS FOR EACH PLAYER

    void Start()
    {
		if(controls == null) {
            controls = gameObject.GetComponent<PlayerControls>();
        }
		
    }

    void FixedUpdate()
    {
        transform.rotation = Quaternion.identity; //Stop player rotating
    }


	public bool canClimbLadder()
	{
        return ladders.Count > 0;
	}



    //----This stuff stops the player from falling thru platforms and ladders----
    void OnTriggerEnter2D(Collider2D other)
    {
        //Ladder Collision
        if (other.gameObject.CompareTag("Ladder")) {
            ladders.Add(other.gameObject);
        }
        if (other.gameObject.CompareTag("Bottom"))
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        ladders.Remove(other.gameObject);
        var rigidBody = GetComponent<Rigidbody2D>();
        if (other.gameObject.CompareTag("Platform") && rigidBody.velocity.y < -2.0f) {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }

    void OnCollisionExit2D(Collision2D coll)
    {
        if (coll.gameObject.CompareTag("Platform"))
        {
            gameObject.GetComponent<BoxCollider2D>().isTrigger = false;
        }
    }
}
