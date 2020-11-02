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
        scoreText.text = $"Score: {score}";
    }

    public void OnGameOver()
    {
        IsGameOver = true;
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

#if DEVELOPMENT_BUILD || UNITY_EDITOR
    public int DebugScore()
    {
        return score;
    }

    public bool DebugIsGameOver()
    {
        return IsGameOver;
    }

    public void DebugResetGame()
    {
        ResetGame();
    }
#endif
}
