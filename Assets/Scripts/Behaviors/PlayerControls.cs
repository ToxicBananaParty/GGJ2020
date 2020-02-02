using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerControls : MonoBehaviour
{
    public Player player;
    public StationControls stationControls;
    public KeyCode left = KeyCode.A;
    public KeyCode right = KeyCode.D;
    public KeyCode up = KeyCode.W;
    public KeyCode down = KeyCode.S;
    public KeyCode jump = KeyCode.Space;
    public KeyCode interact = KeyCode.F;
    public KeyCode secondaryAction = KeyCode.Q;
    public KeyCode tertiaryAction = KeyCode.E;

    private bool climbingLadder = false;

    void Start() {
		if(player == null) {
            player = GetComponent<Player>();
        }
    }

	public void attachStation(StationControls station) {
		if(this.stationControls != null && station != this.stationControls) {
            detachStation(this.stationControls);
		}
        this.stationControls = station;
        station.playerControls = this;
        var rigidBody = player.GetComponent<Rigidbody2D>();
        rigidBody.velocity = Vector3.zero;
        rigidBody.Sleep();
	}

	public void detachStation(StationControls station) {
		if(station != this.stationControls) {
            return;
		}
        this.stationControls = null;
        station.playerControls = null;
        var rigidBody = player.GetComponent<Rigidbody2D>();
        rigidBody.WakeUp();
    }

    void Update()
    {
        if (Input.GetKeyDown(jump) && !climbingLadder) {
            var rigidBody = player.GetComponent<Rigidbody2D>();
            rigidBody.velocity = new Vector2(rigidBody.velocity.x, player.jumpVelocity);
            transform.rotation = Quaternion.identity;
        }
		if (stationControls != null) {
            stationControls.updateControls();
        } else if (Input.GetKeyDown(interact))
        {
            player.GetComponent<Interactor>().interact();
        }
    }

    void FixedUpdate()
    {

        if (stationControls == null) {
            Movement();

        }

        if (player.canClimbLadder() && climbingLadder) {
            player.GetComponent<Rigidbody2D>().gravityScale = 0.0f;
		} else {
            player.GetComponent<Rigidbody2D>().gravityScale = player.gravityScale;
		}
    }


	public void Movement()
    {
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
                var rigidBody = player.GetComponent<Rigidbody2D>();
                rigidBody.velocity = new Vector2(0.0f, 0.0f);
                transform.rotation = Quaternion.identity;
            }
        } else {
            climbingLadder = false;
        }
        transform.rotation = Quaternion.identity;
    }
}
