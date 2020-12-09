using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    private readonly string TutorialCompletedKey = "TutorialCompleted";

    public enum Phase
    {
        NotStarted,
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven,
        End
    }

    //Dependencies
    FieldScript fieldScript;

    private Phase currentPhase;

    private void OnEnable()
    {
        EventManager.OnTutorialMovePhase += MoveCurrentPhase; 
        EventManager.OnClearDots         += OnClearDots;
    }

    private void OnDisable()
    {
        EventManager.OnTutorialMovePhase -= MoveCurrentPhase;
        EventManager.OnClearDots         -= OnClearDots;
    }

    private void Start()
    {
        GetDependencies();

        if (ShouldDisplayTutorial())
        {
            currentPhase = Phase.One;
            PreparePhase(currentPhase);

            EventManager.OnTutorialStart();
        }
    }

    private void GetDependencies()
    {
        fieldScript = GameObject.FindObjectOfType<FieldScript>();
    }

    private bool ShouldDisplayTutorial()
    {
        //return PlayerPrefs.GetInt(TutorialCompletedKey, 0) == 1;
        return true; // TODO: for dev
    }

    private void SetTutorialCompleted()
    {
        PlayerPrefs.SetInt(TutorialCompletedKey, 1);
        PlayerPrefs.Save();
    }

    private void MoveCurrentPhase()
    {
        currentPhase += 1;
        if (currentPhase == Phase.End)
        {
            EventManager.OnTutorialComplete();
        }
        else
        {
            PreparePhase(currentPhase);
        }

    }

    private void PreparePhase(TutorialScript.Phase phase)
    {
        switch (phase)
        {
            case Phase.One:
                PreparePhaseOne();
                break;
            case Phase.Two:
                PreparePhaseOne();
                break;
            case Phase.Three:
                PreparePhaseOne();
                break;
            case Phase.Four:
                PreparePhaseOne();
                break;
            case Phase.Five:
                PreparePhaseOne();
                break;
            case Phase.Six:
                PreparePhaseOne();
                break;
            case Phase.Seven:
                PreparePhaseOne();
                break;
        }
    }

    private void PreparePhaseOne()
    {
        FieldPattern[] patterns =
                        {
                    new FieldPattern(1, 2, DotScript.Type.Star),
                    new FieldPattern(4, 2, DotScript.Type.Star),
                };

        fieldScript.SetPatterns(patterns);
    }

    private void OnClearDots(int count)
    {
        if (ShouldDisplayTutorial())
        {
            if (count > 0)
            {
                MoveCurrentPhase();
            }
        }
    }

#if DEVELOPMENT_BUILD || UNITY_EDITOR
    public bool DebugShouldDisplayTutorial()
    {
        return ShouldDisplayTutorial();
    }

    public Phase DebugCurrentPhase()
    {
        return currentPhase;
    }
#endif
}
