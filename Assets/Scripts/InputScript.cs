using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InputScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Vector2 pos = GetTouchPosition();
        //RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

        if (GetMouseButtonDown())
        {
            //for (int i = 0; i < hits.Length; i++)
            //{
            //    possibleCurrentPick[i] = hits[i].collider.GetComponent<Lane>();
            //}

            //bMouseCoordsOnClick = true;
            //vMouseCoordsOnClick = pos;

            //bMouseCoordsNow = true;
            //vMouseCoordsNow = pos;

            //currentPick = null;

            //pauseTimer = true;
            //timeLeftCurrentScanline = timeBetweenScanLines;
            EventManager.OnTouchStart();
        }
        else if (GetMouseButtonUp())
        {
            //currentPick = null;
            //currentDrop = null;
            //possibleCurrentPick[0] = null;
            //possibleCurrentPick[1] = null;
            //possibleDropId = -1;

            //bMouseCoordsNow = bMouseCoordsOnClick = bCurrentPickTypeLocked = false;
            //pauseTimer = false;
            EventManager.OnTouchEnd();
        }
        else if (GetMouseButton())
        {
            EventManager.OnTouchMove();

            //bMouseCoordsNow = true;
            //vMouseCoordsNow = pos;

            //float dist = Vector3.Distance(vMouseCoordsNow, vMouseCoordsOnClick);
            //float dotProduct = Vector3.Dot(Vector3.right, (vMouseCoordsNow - vMouseCoordsOnClick).normalized);

            //if (!bCurrentPickTypeLocked && dist >= 5.0f)
            //{
            //    float absDotProduct = Mathf.Abs(dotProduct);
            //    if (absDotProduct >= 0.5)
            //    {
            //        currentPickType = Lane.LaneType.Columns;
            //    }
            //    else
            //    {
            //        currentPickType = Lane.LaneType.Row;
            //    }

            //    //find current pick
            //    for (int i = 0; i < possibleCurrentPick.Length; i++)
            //    {
            //        if (possibleCurrentPick[i].laneType == currentPickType)
            //        {
            //            currentPick = possibleCurrentPick[i];
            //        }
            //    }

            //    bCurrentPickTypeLocked = true;

            //}

            //if (!currentPick)
            //{
            //    return;
            //}

            //// find possible drop
            //for (int i = 0; i < hits.Length; i++)
            //{
            //    Lane possibleDrop = hits[i].collider.GetComponent<Lane>();
            //    if (possibleDrop.laneType == currentPickType)
            //    {
            //        currentDrop = possibleDrop;
            //    }
            //}

            //if (possibleDropId != currentDrop.id)
            ////if (true)
            //{
            //    possibleDropId = currentDrop.id;

            //    if (currentPick.id < currentDrop.id)
            //    {
            //        for (int j = currentDrop.id; currentPick.id < j; j--)
            //        {
            //            string a_5 = "";
            //            string a_4 = "";
            //            string a_3 = "";
            //            string a_2 = "";
            //            string a_1 = "";
            //            string a_0 = "";

            //            string b_5 = "";
            //            string b_4 = "";
            //            string b_3 = "";
            //            string b_2 = "";
            //            string b_1 = "";
            //            string b_0 = "";

            //            if (currentPickType == Lane.LaneType.Columns)
            //            {
            //                a_5 = $"5_{j}";
            //                a_4 = $"4_{j}";
            //                a_3 = $"3_{j}";
            //                a_2 = $"2_{j}";
            //                a_1 = $"1_{j}";
            //                a_0 = $"0_{j}";

            //                b_5 = $"5_{j - 1}";
            //                b_4 = $"4_{j - 1}";
            //                b_3 = $"3_{j - 1}";
            //                b_2 = $"2_{j - 1}";
            //                b_1 = $"1_{j - 1}";
            //                b_0 = $"0_{j - 1}";
            //            }
            //            else
            //            {
            //                a_5 = $"{j}_5";
            //                a_4 = $"{j}_4";
            //                a_3 = $"{j}_3";
            //                a_2 = $"{j}_2";
            //                a_1 = $"{j}_1";
            //                a_0 = $"{j}_0";

            //                b_5 = $"{j - 1}_5";
            //                b_4 = $"{j - 1}_4";
            //                b_3 = $"{j - 1}_3";
            //                b_2 = $"{j - 1}_2";
            //                b_1 = $"{j - 1}_1";
            //                b_0 = $"{j - 1}_0";
            //            }

            //            GameObject.Find(a_5).GetComponent<DotScript>().SetNewName(b_5);
            //            GameObject.Find(a_4).GetComponent<DotScript>().SetNewName(b_4);
            //            GameObject.Find(a_3).GetComponent<DotScript>().SetNewName(b_3);
            //            GameObject.Find(a_2).GetComponent<DotScript>().SetNewName(b_2);
            //            GameObject.Find(a_1).GetComponent<DotScript>().SetNewName(b_1);
            //            GameObject.Find(a_0).GetComponent<DotScript>().SetNewName(b_0);
            //        }
            //    }
            //    else if (currentPick.id > currentDrop.id)
            //    {
            //        for (int j = currentDrop.id; j < currentPick.id; j++)
            //        {

            //            string a_5 = "";
            //            string a_4 = "";
            //            string a_3 = "";
            //            string a_2 = "";
            //            string a_1 = "";
            //            string a_0 = "";

            //            string b_5 = "";
            //            string b_4 = "";
            //            string b_3 = "";
            //            string b_2 = "";
            //            string b_1 = "";
            //            string b_0 = "";

            //            if (currentPickType == Lane.LaneType.Columns)
            //            {
            //                a_5 = $"5_{j}";
            //                a_4 = $"4_{j}";
            //                a_3 = $"3_{j}";
            //                a_2 = $"2_{j}";
            //                a_1 = $"1_{j}";
            //                a_0 = $"0_{j}";

            //                b_5 = $"5_{j + 1}";
            //                b_4 = $"4_{j + 1}";
            //                b_3 = $"3_{j + 1}";
            //                b_2 = $"2_{j + 1}";
            //                b_1 = $"1_{j + 1}";
            //                b_0 = $"0_{j + 1}";
            //            }
            //            else
            //            {
            //                a_5 = $"{j}_5";
            //                a_4 = $"{j}_4";
            //                a_3 = $"{j}_3";
            //                a_2 = $"{j}_2";
            //                a_1 = $"{j}_1";
            //                a_0 = $"{j}_0";

            //                b_5 = $"{j + 1}_5";
            //                b_4 = $"{j + 1}_4";
            //                b_3 = $"{j + 1}_3";
            //                b_2 = $"{j + 1}_2";
            //                b_1 = $"{j + 1}_1";
            //                b_0 = $"{j + 1}_0";
            //            }

            //            GameObject.Find(a_5).GetComponent<DotScript>().SetNewName(b_5);
            //            GameObject.Find(a_4).GetComponent<DotScript>().SetNewName(b_4);
            //            GameObject.Find(a_3).GetComponent<DotScript>().SetNewName(b_3);
            //            GameObject.Find(a_2).GetComponent<DotScript>().SetNewName(b_2);
            //            GameObject.Find(a_1).GetComponent<DotScript>().SetNewName(b_1);
            //            GameObject.Find(a_0).GetComponent<DotScript>().SetNewName(b_0);
            //        }
            //    }

            //    if (currentPickType == Lane.LaneType.Columns)
            //    {
            //        GameObject.Find($"5_{currentPick.id}").GetComponent<DotScript>().SetNewName($"5_{currentDrop.id}");
            //        GameObject.Find($"4_{currentPick.id}").GetComponent<DotScript>().SetNewName($"4_{currentDrop.id}");
            //        GameObject.Find($"3_{currentPick.id}").GetComponent<DotScript>().SetNewName($"3_{currentDrop.id}");
            //        GameObject.Find($"2_{currentPick.id}").GetComponent<DotScript>().SetNewName($"2_{currentDrop.id}");
            //        GameObject.Find($"1_{currentPick.id}").GetComponent<DotScript>().SetNewName($"1_{currentDrop.id}");
            //        GameObject.Find($"0_{currentPick.id}").GetComponent<DotScript>().SetNewName($"0_{currentDrop.id}");
            //    }
            //    else
            //    {
            //        GameObject.Find($"{currentPick.id}_5").GetComponent<DotScript>().SetNewName($"{currentDrop.id}_5");
            //        GameObject.Find($"{currentPick.id}_4").GetComponent<DotScript>().SetNewName($"{currentDrop.id}_4");
            //        GameObject.Find($"{currentPick.id}_3").GetComponent<DotScript>().SetNewName($"{currentDrop.id}_3");
            //        GameObject.Find($"{currentPick.id}_2").GetComponent<DotScript>().SetNewName($"{currentDrop.id}_2");
            //        GameObject.Find($"{currentPick.id}_1").GetComponent<DotScript>().SetNewName($"{currentDrop.id}_1");
            //        GameObject.Find($"{currentPick.id}_0").GetComponent<DotScript>().SetNewName($"{currentDrop.id}_0");
            //    }

            //    DotScript[] dots = GameObject.FindObjectsOfType<DotScript>();
            //    foreach (DotScript dot in dots)
            //    {
            //        dot.SwapName();
            //    }

            //    UpdateConnections();
            //}


            //// update current pick
            //for (int i = 0; i < hits.Length; i++)
            //{
            //    possibleCurrentPick[i] = hits[i].collider.GetComponent<Lane>();
            //}

            //// find current pick
            //for (int i = 0; i < possibleCurrentPick.Length; i++)
            //{
            //    if (possibleCurrentPick[i].laneType == currentPickType)
            //    {
            //        currentPick = possibleCurrentPick[i];
            //    }
            //}
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
