using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class Scoring : MonoBehaviour
{
    static public int newScore = 0;
    public Text PlayerScore;
    public Text PlayerHighScore;
    public Text BallCount;

    public void Start()
    {
        newScore = ScoreBoard.finalScore;
        PlayerScore.text = PlayerName.playerName + ' ' + newScore.ToString();
        PlayerHighScore.text = PlayerName.playerName + ' ' + PlayerPrefs.GetInt("HighScore", 0).ToString();
        BallCount.text = DestroyBall.newBallCount.ToString();

        if (newScore >= PlayerPrefs.GetInt("HighScore", 0))
        {
            PlayerPrefs.SetInt("HighScore", newScore);
            PlayerHighScore.text = PlayerPrefs.GetInt("HighScore").ToString();
        }
    }
}
