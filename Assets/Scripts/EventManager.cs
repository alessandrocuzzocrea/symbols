using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LollerClass
{
    public delegate void TestDelegate();

}

public class EventManager : MonoBehaviour
{
    public static LollerClass.TestDelegate OnTestDelegate;
}
