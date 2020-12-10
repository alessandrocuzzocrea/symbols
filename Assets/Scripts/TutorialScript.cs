﻿using System.Collections;
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
                PreparePhaseTwo();
                break;
            case Phase.Three:
                PreparePhaseThree();
                break;
            case Phase.Four:
                PreparePhaseFour();
                break;
            case Phase.Five:
                PreparePhaseFive();
                break;
            case Phase.Six:
                PreparePhaseSix();
                break;
        }
    }

    private void PreparePhaseOne()
    {
        fieldScript.SetPatterns(new FieldPattern[] {
                    new FieldPattern(1, 2, DotScript.Type.Circle),
                    new FieldPattern(4, 2, DotScript.Type.Circle),
                }
        );
    }

    private void PreparePhaseTwo()
    {
        fieldScript.SetPatterns(new FieldPattern[] {
                    new FieldPattern(0, 4, DotScript.Type.Square),
                    new FieldPattern(5, 4, DotScript.Type.Square),
                }
        );
    }

    private void PreparePhaseThree()
    {
        fieldScript.SetPatterns(new FieldPattern[] {
                    new FieldPattern(1, 2, DotScript.Type.Diamond),
                    new FieldPattern(4, 2, DotScript.Type.Diamond),
                    new FieldPattern(1, 4, DotScript.Type.Diamond),
                    new FieldPattern(4, 4, DotScript.Type.Diamond),
                }
        );
    }

    private void PreparePhaseFour()
    {
        fieldScript.SetPatterns(new FieldPattern[] {
                    new FieldPattern(2, 1, DotScript.Type.Circle),
                    new FieldPattern(2, 4, DotScript.Type.Circle),
                }
        );
    }

    private void PreparePhaseFive()
    {
        fieldScript.SetPatterns(new FieldPattern[] {
                    new FieldPattern(4, 0, DotScript.Type.Square),
                    new FieldPattern(4, 5, DotScript.Type.Square),
                }
        );
    }

    private void PreparePhaseSix()
    {
        fieldScript.SetPatterns(new FieldPattern[] {
                    new FieldPattern(2, 1, DotScript.Type.Diamond),
                    new FieldPattern(2, 4, DotScript.Type.Diamond),
                    new FieldPattern(4, 1, DotScript.Type.Diamond),
                    new FieldPattern(4, 4, DotScript.Type.Diamond),
                }
        );
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
