using System;
using System.Collections.Generic;
using UnityEngine;

public class GameplayEvents
{
    public delegate void Start();
    public delegate void Pause();
    public delegate void Reset();
    public delegate void Gameover();
    public delegate void IncreaseScore(int v);
    public delegate void LineCleared(List<DotScript> list, Color color, int score);
    public delegate void DotsCleared(int count);
}
