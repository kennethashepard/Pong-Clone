using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviour {
    [SerializeField]
    float forceValue = 4.5f;
    Rigidbody2D myBody;

    public void Reset()
    {
        //reset the ball position and restart ball movement
        myBody.velocity = Vector2.zero;
        transform.position = new Vector2(0, 0);
        Start();
    }

    public void Stop()
    {
        //this method stops the ball
        myBody.velocity = Vector2.zero;
    }

	// Use this for initialization
	void Start()
    {
        myBody = GetComponent<Rigidbody2D>();
        myBody.AddForce(new Vector2(forceValue * 50, 50));
        forceValue = forceValue * -1;
    }
}
