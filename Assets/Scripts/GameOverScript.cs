﻿using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverUIRoot;

    [SerializeField]
    TextMeshProUGUI scoreText;

    [SerializeField]
    TextMeshProUGUI bestText;

    private readonly string BestScoreKey = "BestScore";

    public void Show(int score)
    {
        SaveBestScore(score);

        scoreText.text = score.ToString();
        bestText.text  = LoadBestScore().ToString();

        gameOverUIRoot.SetActive(true);
    }

    private void SaveBestScore(int newBest)
    {
        if (newBest > LoadBestScore())
        {
            PlayerPrefs.SetInt(BestScoreKey, newBest);
            PlayerPrefs.Save();
        }
    }

    private int LoadBestScore()
    {
        return PlayerPrefs.GetInt(BestScoreKey, 0);
    }
}