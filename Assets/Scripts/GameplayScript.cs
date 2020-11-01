using UnityEngine;
using UnityEngine.UI;

public class GameplayScript : MonoBehaviour
{
    [SerializeField]
    private int score;

    [SerializeField]
    private Text scoreText;

    private void OnEnable()
    {
        EventManager.OnIncreaseScore += IncreaseScore;
    }

    private void OnDisable()
    {
        EventManager.OnIncreaseScore -= IncreaseScore;
    }

    public void IncreaseScore()
    {
        score++;
        scoreText.text = score.ToString();
    }

    void OnGUI()
    {
        GUI.Label(new Rect(10, 97, 1000, 90), "Score: " + score);
    }
}
