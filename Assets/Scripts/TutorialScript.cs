using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    private readonly string TutorialCompletedKey = "TutorialCompleted";

    public enum Phase
    {
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
    }

    private void OnDisable()
    {
        EventManager.OnTutorialMovePhase -= MoveCurrentPhase;
    }

    private void Start()
    {
        GetDependencies();

        if (ShouldDisplayTutorial())
        {

            currentPhase = Phase.One;
            PreparePhase(currentPhase);

            Debug.Log("Start TUT");
            EventManager.OnTutorialStart();
        }
    }

    //private void Update()
    //{
    //    MoveCurrentPhase();
    //}

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
                FieldPattern[] patterns =
                {
                    new FieldPattern(4, 0, DotScript.Type.Star),
                    new FieldPattern(4, 2, DotScript.Type.Star),
                };

                fieldScript.SetPatterns(patterns);
                break;
        }
    }
}
