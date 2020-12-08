using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialScript : MonoBehaviour
{
    private readonly string TutorialCompletedKey = "TutorialCompleted";

    enum Phase
    {
        One,
        Two,
        Three,
        Four,
        Five,
        Six,
        Seven
    }

    //Dependencies
    FieldScript fieldScript;

    private Phase currentPhase;

    void Start()
    {
        GetDependencies();
    }

    private void OnEnable()
    {
        if (ShouldDisplayTutorial())
        {
            currentPhase = Phase.One;

            PreparePhase(currentPhase);
        }
    }

    private void OnDisable()
    {
        
    }

    private void GetDependencies()
    {
        fieldScript = GameObject.FindObjectOfType<FieldScript>();
    }

    private bool ShouldDisplayTutorial()
    {
        return PlayerPrefs.GetInt(TutorialCompletedKey, 0) == 1;
    }

    private void SetTutorialCompleted()
    {
        PlayerPrefs.SetInt(TutorialCompletedKey, 1);
        PlayerPrefs.Save();
    }

    private void MoveCurrentPhase()
    {

    }

    private void PreparePhase(TutorialScript.Phase phase)
    {
        
    }
}
