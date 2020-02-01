using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Player player;
    public StationControls stationControls;
    public KeyCode up, down, left, right, jump;

    private bool climbingLadder = false;

    void Start()
    {
        left = KeyCode.A;
        right = KeyCode.D;
        up = KeyCode.W;
        down = KeyCode.S;
        jump = KeyCode.Space;
        stationControls = null;
		if(player == null) {
            player = GetComponent<Player>();
		}
    }

    void FixedUpdate()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {
            player.GetComponent<Interactor>().interact();
        }

        if (stationControls == null) {
            Movement();

        }
        else {
            //Do station controls
        }

        if (player.canClimbLadder() && climbingLadder) {
            player.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
		} else {
            player.GetComponent<Rigidbody2D>().gravityScale = player.gravityScale;
		}
    }


	public void Movement()
    {
        var rigidBody = player.GetComponent<Rigidbody2D>();
        if (Input.GetKey(left)) {
            player.transform.position += new Vector3(-player.walkSpeed, 0, 0);
            player.GetComponent<SpriteRenderer>().flipX = false;
        }
        if (Input.GetKey(right)) {
            player.transform.position += new Vector3(player.walkSpeed, 0, 0);
            player.GetComponent<SpriteRenderer>().flipX = true;
        }

		// climbing
		if(player.canClimbLadder()) {
            if (Input.GetKey(up)) {
                climbingLadder = true;
                player.transform.position += new Vector3(0, player.climbSpeed, 0);
            }
			if(Input.GetKey(down)) {
                player.transform.position += new Vector3(0, -player.climbSpeed, 0);
            }
            var magnetic = player.GetComponent<Magnetic>();
            if (climbingLadder && (magnetic == null || !magnetic.isStuckToMagnet())) {
                rigidBody.velocity = new Vector2(0.0f, 0.0f);
            }
        } else {
            climbingLadder = false;
            if (Input.GetKeyDown(jump)) {
                rigidBody.velocity = new Vector2(rigidBody.velocity.x, player.jumpVelocity);
            }
        }
    }
}
