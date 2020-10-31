using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventManager : MonoBehaviour
{
    public static TimerEvents.End OnTimerEnd;

    public static TouchEvents.End OnTouchStart;
    public static TouchEvents.End OnTouchMove;
    public static TouchEvents.End OnTouchEnd;
}
