using UnityEngine;
using UnityEngine.SceneManagement;

public class AppScript : MonoBehaviour
{
    private void Awake()
    {
        //Initial Setup
        Application.targetFrameRate = 60;
    }

    private void OnEnable()
    {
        EventManager.OnReset += ResetGame;
    }

    private void OnDisable()
    {
        EventManager.OnReset -= ResetGame;
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0);
    }

#if DEVELOPMENT_BUILD || UNITY_EDITOR
    public void DebugResetGame()
    {
        ResetGame();
    }
#endif
}
