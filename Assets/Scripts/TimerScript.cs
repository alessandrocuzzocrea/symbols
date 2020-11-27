using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public bool pauseTimer;
    public float timeLeftCurrentScanline;
    public float timeBetweenScanLines = 1.0f;
    public Image timerImage;
    public int Turn { get; private set; }
    public float currentTimeMultiplier;

    private void OnEnable()
    {
        //EventManager.OnTouchStart += Pause;
        //EventManager.OnTouchEnd   += Resume;

        EventManager.OnTouchStart += SlowDown;
        EventManager.OnTouchEnd   += SlowUp;

        EventManager.OnGameOver += Pause;
    }

    private void OnDisable()
    {
        //EventManager.OnTouchStart -= Pause;
        //EventManager.OnTouchEnd   -= Resume;

        EventManager.OnTouchStart -= SlowDown;
        EventManager.OnTouchEnd   -= SlowUp;

        EventManager.OnGameOver -= Pause;

    }

    void Start()
    {
        Turn = 1;
        currentTimeMultiplier = 1.0f;
        Reset();
    }

    void Update()
    {
        if (!pauseTimer)
        {
            timeLeftCurrentScanline -= Time.smoothDeltaTime * currentTimeMultiplier;
        }

        if (timeLeftCurrentScanline <= 0)
        {
            EventManager.OnTimerEnd?.Invoke();
            Turn++;
            Reset();
        }

        timerImage.fillAmount = timeLeftCurrentScanline;
    }

    private void Pause()
    {
        Reset();
        pauseTimer = true;
    }

    private void Resume()
    {
        pauseTimer = false;
    }

    private void SlowUp()
    {
        currentTimeMultiplier = 1.0f;
    }

    private void SlowDown()
    {
        currentTimeMultiplier = 0.5f;
    }

    private void Reset()
    {
        timeLeftCurrentScanline = timeBetweenScanLines;
    }

#if DEVELOPMENT_BUILD || UNITY_EDITOR
    public float DebugTimeLeftCurrentScanline()
    {
        return timeLeftCurrentScanline;
    }
#endif
}
