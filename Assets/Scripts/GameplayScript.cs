using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class GameplayScript : MonoBehaviour
{
    private int score;
    public bool IsGameOver { get; private set; }

    [SerializeField]
    private TextMeshProUGUI scoreTextTMP;

    [SerializeField]
    private int[] scores;

    // Dependencies
    private ScoreScript    scoreScript;
    private GameOverScript gameOverScript;

    private void OnEnable()
    {
        EventManager.OnIncreaseScore += IncreaseScore;
        EventManager.OnGameOver      += OnGameOver;
    }

    private void OnDisable()
    {
        EventManager.OnIncreaseScore -= IncreaseScore;
        EventManager.OnGameOver      -= OnGameOver;
    }

    private void GetDependencies()
    {
        scoreScript    = GameObject.FindObjectOfType<ScoreScript>();
        gameOverScript = GameObject.FindObjectOfType<GameOverScript>();
    }

    private void Start()
    {
        GetDependencies();
    }

    public void IncreaseScore(int v)
    {
        score += v;
        scoreScript.IncreaseScore(v);
    }

    public void OnGameOver()
    {
        IsGameOver = true;
        gameOverScript.Show(score);
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    public int GetScoreForCombo(int c)
    {
        return scores[c - 1];
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

    public void DebugGameOver()
    {
        OnGameOver();
    }
#endif
}
