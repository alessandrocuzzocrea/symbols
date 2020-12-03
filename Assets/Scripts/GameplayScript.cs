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
    private ScoreScript scoreScript;

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
        scoreScript = GameObject.FindObjectOfType<ScoreScript>();
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
