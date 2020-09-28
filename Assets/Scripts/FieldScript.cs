using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScript : MonoBehaviour
{
    public Object prefab;
    public int rows;
    public int columns;
    public float spacing;
    public Vector2[,] positions;

    public DotScript pick1;
    public DotScript pick2;

    //Scanline
    public Object scanLinePrefab;
    public Transform scanlineTransform;
    public float timeBetweenScanLines = 4.0f;
    public float timeLeftCurrentScanline;
    public int currentRow;

    //Setup
    //public int initialDotsCount = 6;

    // Start is called before the first frame update
    void Start()
    {
        positions = new Vector2[rows, columns];

        for (int j = 0; j < columns; j++)
        {
            for (int i = 0; i < rows; i++)
            {
                GameObject child = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform) as GameObject;
                Vector2 position = new Vector2(j * 1, i * 1);
                child.transform.localPosition = position;
                positions[i, j] = position;
                DotScript script = child.GetComponent<DotScript>();
                script.field = this;
                script.currentRow = i;
                script.currentColumn = j;
                script.SetType(DotScript.Type.Empty);
            }
        }

        int a = transform.childCount;
        //int dotsRemainingToPlace = initialDotsCount;

        for (int i = 0; i < a; i++)
        {
            if (Random.Range(0.0f, 1.0f) >= .8f)
            {
                transform.GetChild(i).GetComponent<DotScript>().SetType(DotScript.Type.Red);
                //dotsRemainingToPlace -= 1;
            }

            //if (dotsRemainingToPlace <= 0)
            //{
            //    break;
            //}
        }

        //Init scanline
        timeLeftCurrentScanline = timeBetweenScanLines;
        currentRow = rows - 1;
        GameObject scanLine = Instantiate(scanLinePrefab, transform) as GameObject;
        scanlineTransform = scanLine.transform;
        Vector2 localPosition = scanlineTransform.localPosition;
        localPosition.y = currentRow;
        scanlineTransform.localPosition = localPosition;

        UpdateConnectionsOnStart();
    }

    IEnumerator UpdateConnectionsOnStart()
    {
        yield return 0;
        UpdateConnections();
    }

    // Update is called once per frame
    void Update()
    {
        //Update timer
        timeLeftCurrentScanline -= Time.smoothDeltaTime;
        if (timeLeftCurrentScanline <= 0)
        {
            timeLeftCurrentScanline = timeBetweenScanLines;
            currentRow -= 1;
            if (currentRow < 0)
            {
                currentRow = rows - 1;
            }

            Vector2 localPosition = scanlineTransform.localPosition;
            localPosition.y = currentRow;
            scanlineTransform.localPosition = localPosition;
        }

        //Debug.Log($"update: {name}");
        //if (Input.GetMouseButtonDown(0))
        //{
        //    Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        //    RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

        //    if (hit)
        //    {
        //        Debug.Log(hit.transform.gameObject.name);

        //        if (pick1 == hit.transform.gameObject.GetComponent<DotScript>() || pick2 == hit.transform.gameObject.GetComponent<DotScript>())
        //        {
        //            Debug.Log("Already picked");
        //            return;
        //        }

        //        if ( pick1 == null )
        //        {
        //            pick1 = hit.transform.gameObject.GetComponent<DotScript>();
        //            return;
        //        }

        //        if ( pick2 == null )
        //        {
        //            pick2 = hit.transform.gameObject.GetComponent<DotScript>();
        //            //return;
        //        }

        //        if (pick1 && pick2) { 
        //            Color color1 = pick1.GetComponent<SpriteRenderer>().color;
        //            Color color2 = pick2.GetComponent<SpriteRenderer>().color;

        //            pick1.GetComponent<SpriteRenderer>().color = color2;
        //            pick2.GetComponent<SpriteRenderer>().color = color1;

        //            pick1 = null;
        //            pick2 = null;
        //        }
        //    }
        //}

        //Draw debug stuff
        for (int j = 0; j < columns; j++)
        {
            for (int i = 0; i < rows; i++)
            {
                Vector2 start = new Vector2(j * 1, i * 1);

                //Horizontal
                Vector2 endH = new Vector2(start.x + 2, start.y);
                Debug.DrawLine(start, endH);

                //Vertical
                Vector2 endV = new Vector2(start.x, start.y - 2);
                Debug.DrawLine(start, endV);

                //GameObject child = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform) as GameObject;
                //child.transform.localPosition = new Vector3(j * 1, i * 1);
                //child.name = $"Dot_{j}_{i}";
                //DotScript script = child.GetComponent<DotScript>();
                //script.field = this;
            }
        }
    }

    void MoveDots(string row, string direction)
    {
        switch(direction)
        {
            case "U":
                {
                    GameObject test5 = GameObject.Find($"5_{row[2]}");
                    GameObject test4 = GameObject.Find($"4_{row[2]}");
                    GameObject test3 = GameObject.Find($"3_{row[2]}");
                    GameObject test2 = GameObject.Find($"2_{row[2]}");
                    GameObject test1 = GameObject.Find($"1_{row[2]}");
                    GameObject test0 = GameObject.Find($"0_{row[2]}");

                    DotScript.Type type5 = test5.GetComponent<DotScript>().color;
                    DotScript.Type type4 = test4.GetComponent<DotScript>().color;
                    DotScript.Type type3 = test3.GetComponent<DotScript>().color;
                    DotScript.Type type2 = test2.GetComponent<DotScript>().color;
                    DotScript.Type type1 = test1.GetComponent<DotScript>().color;
                    DotScript.Type type0 = test0.GetComponent<DotScript>().color;

                    test5.GetComponent<DotScript>().SetType(type4);
                    test4.GetComponent<DotScript>().SetType(type3);
                    test3.GetComponent<DotScript>().SetType(type2);
                    test2.GetComponent<DotScript>().SetType(type1);
                    test1.GetComponent<DotScript>().SetType(type0);
                    test0.GetComponent<DotScript>().SetType(type5);
                }
                break;

            case "D":
                {
                    GameObject test5 = GameObject.Find($"5_{row[2]}");
                    GameObject test4 = GameObject.Find($"4_{row[2]}");
                    GameObject test3 = GameObject.Find($"3_{row[2]}");
                    GameObject test2 = GameObject.Find($"2_{row[2]}");
                    GameObject test1 = GameObject.Find($"1_{row[2]}");
                    GameObject test0 = GameObject.Find($"0_{row[2]}");

                    DotScript.Type type5 = test5.GetComponent<DotScript>().color;
                    DotScript.Type type4 = test4.GetComponent<DotScript>().color;
                    DotScript.Type type3 = test3.GetComponent<DotScript>().color;
                    DotScript.Type type2 = test2.GetComponent<DotScript>().color;
                    DotScript.Type type1 = test1.GetComponent<DotScript>().color;
                    DotScript.Type type0 = test0.GetComponent<DotScript>().color;

                    test5.GetComponent<DotScript>().SetType(type0);
                    test4.GetComponent<DotScript>().SetType(type5);
                    test3.GetComponent<DotScript>().SetType(type4);
                    test2.GetComponent<DotScript>().SetType(type3);
                    test1.GetComponent<DotScript>().SetType(type2);
                    test0.GetComponent<DotScript>().SetType(type1);
                }
                break;

            case "L":
                {
                    GameObject test5 = GameObject.Find($"{row[0]}_5");
                    GameObject test4 = GameObject.Find($"{row[0]}_4");
                    GameObject test3 = GameObject.Find($"{row[0]}_3");
                    GameObject test2 = GameObject.Find($"{row[0]}_2");
                    GameObject test1 = GameObject.Find($"{row[0]}_1");
                    GameObject test0 = GameObject.Find($"{row[0]}_0");

                    DotScript.Type type5 = test5.GetComponent<DotScript>().color;
                    DotScript.Type type4 = test4.GetComponent<DotScript>().color;
                    DotScript.Type type3 = test3.GetComponent<DotScript>().color;
                    DotScript.Type type2 = test2.GetComponent<DotScript>().color;
                    DotScript.Type type1 = test1.GetComponent<DotScript>().color;
                    DotScript.Type type0 = test0.GetComponent<DotScript>().color;

                    test5.GetComponent<DotScript>().SetType(type0);
                    test4.GetComponent<DotScript>().SetType(type5);
                    test3.GetComponent<DotScript>().SetType(type4);
                    test2.GetComponent<DotScript>().SetType(type3);
                    test1.GetComponent<DotScript>().SetType(type2);
                    test0.GetComponent<DotScript>().SetType(type1);
                }
                break;

            case "R":
                {
                    GameObject test5 = GameObject.Find($"{row[0]}_5");
                    GameObject test4 = GameObject.Find($"{row[0]}_4");
                    GameObject test3 = GameObject.Find($"{row[0]}_3");
                    GameObject test2 = GameObject.Find($"{row[0]}_2");
                    GameObject test1 = GameObject.Find($"{row[0]}_1");
                    GameObject test0 = GameObject.Find($"{row[0]}_0");

                    DotScript.Type type5 = test5.GetComponent<DotScript>().color;
                    DotScript.Type type4 = test4.GetComponent<DotScript>().color;
                    DotScript.Type type3 = test3.GetComponent<DotScript>().color;
                    DotScript.Type type2 = test2.GetComponent<DotScript>().color;
                    DotScript.Type type1 = test1.GetComponent<DotScript>().color;
                    DotScript.Type type0 = test0.GetComponent<DotScript>().color;

                    test5.GetComponent<DotScript>().SetType(type4);
                    test4.GetComponent<DotScript>().SetType(type3);
                    test3.GetComponent<DotScript>().SetType(type2);
                    test2.GetComponent<DotScript>().SetType(type1);
                    test1.GetComponent<DotScript>().SetType(type0);
                    test0.GetComponent<DotScript>().SetType(type5);
                }
                break;
            default:
                break;
        }
    }

    void UpdateConnections()
    {
        // Reset all connections
        for (int i = 0; i < transform.childCount; i++)
        {
            DotScript dot = transform.GetChild(i).GetComponent<DotScript>();
            if (dot)
            {
                dot.connectedTo = null;
            }
        }

        // Check for new connections
        for (int j = 0; j < columns - 1; j++)
        {
            for (int i = 0; i < rows; i++)
            {
                DotScript child = GameObject.Find($"{i}_{j}").GetComponent<DotScript>();

                if (child.color != DotScript.Type.Empty)
                {
                    DotScript neighbour = GameObject.Find($"{i}_{j+1}").GetComponent<DotScript>();

                    if (child.color == neighbour.color)
                    {
                        child.connectedTo = neighbour;
                    }
                }
            }
        }
    }

    void OnGUI()
    {

        GUI.Label(new Rect(10, 0, 1000, 90), $"Time: {timeLeftCurrentScanline}");
        if (pick1) GUI.Label(new Rect(10, 16, 1000, 90), pick1.name);
        if (pick2) GUI.Label(new Rect(10, 26, 1000, 90), pick2.name);

        //Debug buttons
        string[] topRow = { "5_0", "5_1", "5_2", "5_3", "5_4", "5_5" };
        foreach(string row in topRow)
        {
            GameObject test = GameObject.Find(row);
            Vector2 position = Camera.main.WorldToScreenPoint(test.transform.position);
            if (GUI.Button(new Rect(position.x, Screen.height - position.y - 40, 20, 20), "U"))
            {
                MoveDots(row, "U");
                UpdateConnections();
            }
        }

        string[] bottomRow = { "0_0", "0_1", "0_2", "0_3", "0_4", "0_5" };
        foreach (string row in bottomRow)
        {
            GameObject test = GameObject.Find(row);
            Vector2 position = Camera.main.WorldToScreenPoint(test.transform.position);
            if (GUI.Button(new Rect(position.x, Screen.height - position.y + 20, 20, 20), "D"))
            {
                MoveDots(row, "D");
                UpdateConnections();
            }
        }

        string[] leftRow = { "0_0", "1_0", "2_0", "3_0", "4_0", "5_0" };
        foreach (string row in leftRow)
        {
            GameObject test = GameObject.Find(row);
            Vector2 position = Camera.main.WorldToScreenPoint(test.transform.position);
            if (GUI.Button(new Rect(position.x - 40, Screen.height - position.y, 20, 20), "L"))
            {
                MoveDots(row, "L");
                UpdateConnections();
            }
        }

        string[] rightRow = { "0_5", "1_5", "2_5", "3_5", "4_5", "5_5" };
        foreach (string row in rightRow)
        {
            GameObject test = GameObject.Find(row);
            Vector2 position = Camera.main.WorldToScreenPoint(test.transform.position);
            if (GUI.Button(new Rect(position.x + 20, Screen.height - position.y, 20, 20), "R"))
            {
                MoveDots(row, "R");
                UpdateConnections();
            }
        }
    }
}
