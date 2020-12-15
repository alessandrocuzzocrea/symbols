using UnityEngine;
using UnityEngine.SceneManagement;

public class AppScript : MonoBehaviour
{
    private void Awake()
    {
        SetTargetFrameRate();

        if (IsIphone())
        {
            LockOrientation();
        }
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

    private void SetTargetFrameRate()
    {
        Application.targetFrameRate = 60;
    }

    private bool IsIphone()
    {
        if (SystemInfo.deviceModel.Contains("iPhone"))
        {
            return true;
        }

        return false;
    }

    private void LockOrientation()
    {
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
        Screen.orientation = ScreenOrientation.Portrait;
    }

#if DEVELOPMENT_BUILD || UNITY_EDITOR
    public void DebugResetGame()
    {
        ResetGame();
    }
#endif
}
