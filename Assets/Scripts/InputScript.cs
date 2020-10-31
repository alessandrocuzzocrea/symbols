using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
    void Update()
    {
        if (GetMouseButtonDown())
        {
            EventManager.OnTouchStart();
        }
        else if (GetMouseButtonUp())
        {
            EventManager.OnTouchEnd();
        }
        else if (GetMouseButton())
        {
            EventManager.OnTouchMove();
        }
    }

    public static Vector2 GetTouchPosition()
    {
        if (Input.touchSupported)
        {
            if (Input.touchCount > 0)
            {
                return Input.GetTouch(0).position;
            }
        }

        return Input.mousePosition;
    }


    private bool GetMouseButtonDown()
    {
        if (Input.touchSupported)
        {
            if (Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(0);
                return t.phase == TouchPhase.Began;
            }
        }

        return Input.GetMouseButtonDown(0);
    }

    private bool GetMouseButtonUp()
    {
        if (Input.touchSupported)
        {
            if (Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(0);
                return t.phase == TouchPhase.Ended || t.phase == TouchPhase.Canceled;
            }
        }

        return Input.GetMouseButtonUp(0);
    }

    private bool GetMouseButton()
    {
        if (Input.touchSupported)
        {
            if (Input.touchCount > 0)
            {
                Touch t = Input.GetTouch(0);
                return t.phase == TouchPhase.Moved || t.phase == TouchPhase.Stationary;
            }
        }

        return Input.GetMouseButton(0);
    }
}
