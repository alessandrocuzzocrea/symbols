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

    private void Start()
    {
        UpdateUI();
    }

    public void IncreaseScore()
    {
        score++;
        UpdateUI();
    }

    public void OnGameOver()
    {
        IsGameOver = true;
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

    private void UpdateUI()
    {
        scoreTextTMP.text = score.ToString();
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
