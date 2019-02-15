using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoalScript : MonoBehaviour {
    [SerializeField]
    int attackingPlayer; //which player scores into this goal
    [SerializeField]
    GameManagerScript gameMan; //this will hold a reference to the game manager script

    void OnCollisionEnter2D(Collision2D other)
    {
        if(other.transform.name == "Ball")
        {
            gameMan.GoalScored(attackingPlayer);
        }
    }

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
