using System.Collections;
using System.Collections.Generic;
using TMPro;
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
    public TextMeshProUGUI scoreTextTMP;
    public TextMeshProUGUI scoreLabelTMP;
    public Image timerImage;
    public Image timerDotImage;
    //public Color currentColor;
    //public Color normalColor;
    //public Color halfColor;
    //public Color quarterColor;
    //public Color lessThen10Color;

    public Gradient colorGradient;

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
        //currentColor = normalColor;
        Reset();
        UpdateUI();
    }

    void Update()
    {
        if (!pauseTimer)
        {
            timeLeftCurrentScanline -= Time.smoothDeltaTime;
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
        Time.timeScale = normalTimeMultiplier;
    }

    private void SlowDown()
    {
        Time.timeScale = slowedDownTimeMultiplier;
    }

    private void Reset()
    {
        timeLeftCurrentScanline = timeBetweenScanLines;
    }

    private void UpdateUI()
    {
        timerImage.fillAmount = timeLeftCurrentScanline;
        timerDotImage.transform.rotation = Quaternion.AngleAxis(360.0f * timeLeftCurrentScanline, Vector3.forward);

        Color c = colorGradient.Evaluate(timeLeftCurrentScanline);
        SetUIColor(c);
    }

    private void SetUIColor(Color c)
    {
        timerImage.color    = c;
        timerDotImage.color = c;
        //scoreLabelTMP.color = c;
        //scoreTextTMP.color  = c;
    }

#if DEVELOPMENT_BUILD || UNITY_EDITOR
    public float DebugTimeLeftCurrentScanline()
    {
        return timeLeftCurrentScanline;
    }

    public void DebugPauseResume()
    {
        pauseTimer = !pauseTimer;
    }
#endif
}
