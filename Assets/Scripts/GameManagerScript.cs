using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameManagerScript : MonoBehaviour {

    [SerializeField]
    BallScript gameBall;
    [SerializeField]
    Text scoreText;
    [SerializeField]
    AudioClip goalScored;
    [SerializeField]
    AudioClip endGame;
    [SerializeField]
    GameObject endGameScreen;

    int playerOneScore;
    int playerTwoScore;
    AudioSource audSource;

	// Use this for initialization
	void Start () {
        audSource = GetComponent<AudioSource>();
        StartNewGame();
	}

    public void StartNewGame()
    {
        playerOneScore = 0;
        playerTwoScore = 0;
        UpdateScoreText();
        endGameScreen.SetActive(false);
        gameBall.Reset();
    }

    public void GoalScored(int playerNumber)
    {
        PlaySound(goalScored);
        //increase the score for whichever player scored
        if (playerNumber == 1)
            playerOneScore++;
        else if (playerNumber == 2)
            playerTwoScore++;

        UpdateScoreText();
        gameBall.Reset();

        //now check if the player has won
        if (playerOneScore == 3)
            GameOver(1);
        else if (playerTwoScore == 3)
            GameOver(2);
    }

    void GameOver(int winner)
    {
        //this is called when a player reaches 3 points
        PlaySound(endGame);
        gameBall.Stop();
        endGameScreen.SetActive(true);
    }

    void UpdateScoreText()
    {
        scoreText.text = "Player One " + playerOneScore.ToString() + " - " + playerTwoScore.ToString() + " Player Two";
    }

    void PlaySound(AudioClip soundClip)
    {
        audSource.clip = soundClip;
        audSource.Play();
    }
}
