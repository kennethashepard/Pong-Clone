using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PaddleScript : MonoBehaviour {

    [SerializeField]
    bool isPlayerTwo;
    [SerializeField]
    float speed = 0.2f;         //How far the paddle moves per frame
    [SerializeField]
    Transform BoundaryHolder;

    Transform myTransform;      //reference to the object's transform
    struct Boundary             //maximum values for player movement so it doesn't go offscreen
    {
        public float Up, Down;

        public Boundary(float up, float down)
        {
            Up = up;
            Down = down;
        }
    }
    Boundary playerBoundary;
    enum Direction {down=-1, stopped, up};
    int ballDirectionUpdate;
    float previousPositionY;
    string player1Up = "q";
    string player1Down = "a";
    string player2Up = "o";
    string player2Down = "l";

	// Use this for initialization
	void Start()
    {
        myTransform = transform;
        previousPositionY = myTransform.position.y;

        //Paddles shouldn't move on the X axis ever.
        playerBoundary = new Boundary((float)(BoundaryHolder.GetChild(0).position.y - 1.33),
                                      (float)(BoundaryHolder.GetChild(1).position.y + 1.33));
    }
	
	// FixedUpdate is called once per frame
	void FixedUpdate()
    {
		// first decide if this is player 1 or player 2 so we know what keys to listen for
        if (isPlayerTwo)
        {
            if (Input.GetKey(player2Up))
                MoveUp();
            else if (Input.GetKey(player2Down))
                MoveDown();
        }
        else // if not player 2 it must be player 1
        {
            if (Input.GetKey(player1Up))
                MoveUp();
            else if (Input.GetKey(player1Down))
                MoveDown();
        }

        //Update the direction variable, so we can affect the ball's future velocity
        if (previousPositionY > myTransform.position.y)
            ballDirectionUpdate = (int)Direction.down;
        else if (previousPositionY < myTransform.position.y)
            ballDirectionUpdate = (int)Direction.up;
        else
            ballDirectionUpdate = (int)Direction.stopped;
    }

    void LateUpdate()
    {
        previousPositionY = myTransform.position.y;
    }

    //move the player's paddle up by an amount determined by 'speed'
    void MoveUp()
    {
        Vector2 clampedPlayerPosition = new Vector2(myTransform.position.x,
                                                    Mathf.Clamp(myTransform.position.y + speed, playerBoundary.Down,
                                                                playerBoundary.Up));
        myTransform.position = clampedPlayerPosition;
    }

    // move the player's paddle down by an amount determined by 'speed'
    void MoveDown()
    {
        Vector2 clampedPlayerPosition = new Vector2(myTransform.position.x,
                                                    Mathf.Clamp(myTransform.position.y - speed, playerBoundary.Down,
                                                                playerBoundary.Up));
        myTransform.position = clampedPlayerPosition;
    }

    //OnCollisionExit2D() runs automatically when a collider within the GameObject the script is attached to comes into contact with another collider.
    //this will change the ball's velocity based on whether the paddle is moving up, down, or not at all.
    void OnCollisionExit2D(Collision2D other)
    {
        float adjust = 5 * ballDirectionUpdate;
        other.rigidbody.velocity = new Vector2(other.rigidbody.velocity.x, other.rigidbody.velocity.y + adjust);
    }
}
