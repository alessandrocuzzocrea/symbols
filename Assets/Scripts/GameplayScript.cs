using UnityEngine;
using UnityEngine.SceneManagement;
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

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 97, 1000, 90), "Score: " + score);
        if (IsGameOver) GUI.Label(new Rect(10, 126, 1000, 90), "GAME OVER");

        if (GUI.Button(new Rect(10, 176, 100, 20), "Reset Game"))
        {
            ResetGame();
        }
    }
}
