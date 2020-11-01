using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    //TODO: ho come l'impressione che tutti sti OnXXX avrebbero piu' senso se rinominati in SendXXXEvent o simili
    public static TimerEvents.End OnTimerEnd;

    public static TouchEvents.Start OnTouchStart;
    public static TouchEvents.Move  OnTouchMove;
    public static TouchEvents.End   OnTouchEnd;

    public static GameplayEvents.Gameover      OnGameOver;
    public static GameplayEvents.IncreaseScore OnIncreaseScore;
}
