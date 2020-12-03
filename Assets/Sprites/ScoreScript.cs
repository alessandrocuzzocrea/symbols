using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class ScoreScript : MonoBehaviour
{
    public TextMeshProUGUI scoreLabelTMP;
    public TextMeshProUGUI scoreTextTMP;
    public TextMeshProUGUI scoreText2TMP;

    public float timer;
    public float debounce;

    public int futureScore;
    public int currScore;

    public  float stepScore;
    private float _futureScore;
    private float _currScore;

    void Start()
    {
        currScore = 12300;
        _currScore = 12300;

        scoreText2TMP.alpha = 0.0f;
    }

    void Update()
    {
        if ( debounce > 0.0f)
        {
            debounce -= Time.deltaTime;

            scoreTextTMP.text = ((int)_currScore).ToString();
            scoreText2TMP.text = '+' + ((int)_futureScore).ToString();
            scoreText2TMP.alpha = 1.0f;

        }
        else if (timer > 0.0f)
        {
            //scoreText2TMP.alpha = 1.0f;

            timer -= Time.deltaTime;
            _currScore += (stepScore * Time.deltaTime);
            _futureScore -= (stepScore * Time.deltaTime);

            if (timer <= 0.0f)
            {
                _currScore = currScore;
                futureScore = 0;
                _futureScore = 0;
                timer = 0.0f;
            }

            if(_futureScore == 0)
            {
                scoreText2TMP.alpha = 0.0f;
            }

            scoreTextTMP.text =  ((int)_currScore).ToString();
            scoreText2TMP.text = '+' + ((int)_futureScore).ToString();

        }
    }

    public void IncreaseScore(int increase)
    {
        var loller = increase;
        var lollerTimer = .25f;

        _futureScore += loller;
        futureScore += loller;

        currScore += loller;


        stepScore = (int)(futureScore / lollerTimer);
        timer = lollerTimer;
        debounce = .5f;
    }
}
