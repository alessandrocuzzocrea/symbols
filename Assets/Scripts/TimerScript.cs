using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public bool pauseTimer;
    public float timeLeftCurrentScanline;
    public float timeBetweenScanLines = 1.0f;
    public int Turn { get; private set; }
    public float normalTimeMultiplier;
    public float slowedDownTimeMultiplier;
    public float currentTimeMultiplier;

    // UI stuff
    public Image timerImage;
    public Image timerDotImage;


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
        currentTimeMultiplier = normalTimeMultiplier;
        Reset();
        UpdateUI();
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

        UpdateUI();
    }

    private void Pause()
    {
        Reset();
        pauseTimer = true;
    }

    //private void Resume()
    //{
    //    pauseTimer = false;
    //}

    private void SlowUp()
    {
        currentTimeMultiplier = normalTimeMultiplier;
    }

    private void SlowDown()
    {
        currentTimeMultiplier = slowedDownTimeMultiplier;
    }

    private void Reset()
    {
        timeLeftCurrentScanline = timeBetweenScanLines;
    }

    private void UpdateUI()
    {
        timerImage.fillAmount = timeLeftCurrentScanline;
        //timerDotImage.transform.rotation = Quaternion.Euler(0, 0, 360.0f / timeLeftCurrentScanline);
        timerDotImage.transform.rotation = Quaternion.AngleAxis(360.0f * timeLeftCurrentScanline, Vector3.forward);
        //rot.z = ;
        //timerDotImage.transform.rotation = rot;
    }

#if DEVELOPMENT_BUILD || UNITY_EDITOR
    public float DebugTimeLeftCurrentScanline()
    {
        return timeLeftCurrentScanline;
    }
#endif
}
