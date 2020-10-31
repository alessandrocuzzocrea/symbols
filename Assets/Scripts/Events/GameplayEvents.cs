using System;
using UnityEngine;

public class GameplayEvents
{
    public delegate void Start();
    public delegate void Pause();
    public delegate void Reset();
    public delegate void Gameover();
}
