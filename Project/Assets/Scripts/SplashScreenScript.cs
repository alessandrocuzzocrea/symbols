using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SplashScreenScript : MonoBehaviour
{
    [SerializeField]
    Animator anim;

    private void OnEnable()
    {
        EventManager.OnPrepareForReset += FadeIn;
    }

    private void OnDisable()
    {
        EventManager.OnPrepareForReset -= FadeIn;
    }

    private void FadeIn()
    {
        anim.SetBool("TransitionToFadeIn", true);
    }
}
