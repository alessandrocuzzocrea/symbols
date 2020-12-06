using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverScript : MonoBehaviour
{
    [SerializeField]
    GameObject gameOverUIRoot;

    public void Show(int score)
    {
        gameOverUIRoot.SetActive(true);
    }
}
