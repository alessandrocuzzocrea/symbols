#pragma warning disable IDE0051

using UnityEngine;

public class SplashScreenAnimationEventScript : MonoBehaviour
{
    private void SplashScreenAlmostDone()
    {
        EventManager.OnSplashScreenAlmostDone();
    }
}
