using UnityEngine;
using UnityEngine.UI;

public class GameplayScript : MonoBehaviour
{
    private int score;
    public bool IsGameOver { get; private set; }

    [SerializeField]
    private Text scoreText;

    private void OnEnable()
    {
        EventManager.OnIncreaseScore += IncreaseScore;
        EventManager.OnGameOver      += OnGameOver;
    }

    private void OnDisable()
    {
        EventManager.OnIncreaseScore -= IncreaseScore;
        EventManager.OnIncreaseScore -= OnGameOver;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    public void OnGameOver()
    {
        IsGameOver = true;
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 97, 1000, 90), "Score: " + score);
    }
}
