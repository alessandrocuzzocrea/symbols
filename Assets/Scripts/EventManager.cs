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
    public static GameplayEvents.LineCleared   OnClearLine;
    public static GameplayEvents.DotsCleared   OnClearDots;

    public static AppEvents.SplashScreenAlmostDone OnSplashScreenAlmostDone;
    public static AppEvents.PrepareForReset        OnPrepareForReset;
    public static AppEvents.Reset                  OnReset;

    public static TutorialEvents.Start     OnTutorialStart;
    public static TutorialEvents.Complete  OnTutorialComplete;
    public static TutorialEvents.MovePhase OnTutorialMovePhase;
}
