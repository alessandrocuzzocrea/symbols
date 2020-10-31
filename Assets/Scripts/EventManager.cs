using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static TimerEvents.End OnTimerEnd;

    public static TouchEvents.Start OnTouchStart;
    public static TouchEvents.Move  OnTouchMove;
    public static TouchEvents.End   OnTouchEnd;
}
