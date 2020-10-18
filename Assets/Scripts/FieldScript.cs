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

    //Crosshair
    public GameObject crosshair;
    bool isDragging;
    Vector2 mousePosAtDragStart;
    GameObject draggedDot;

    int maxDrop = 3;
    //Setup
    //public int initialDotsCount = 6;


    // Prototype 2
    public Lane currentPick;
    public Lane[] possibleCurrentPick;
    public Lane.LaneType currentPickType;
    public bool pauseTimer;
    public int possibleDropId;
    //public string possibleDropString;
    public Lane currentDrop;

    // Start is called before the first frame update
    void Start()
    {
        positions = new Vector2[rows, columns];
        possibleCurrentPick = new Lane[2];


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
                if (transform.GetChild(i).GetComponent<DotScript>())
                {
                    transform.GetChild(i).GetComponent<DotScript>().SetType(DotScript.GetRandomColor());
                }
                //dotsRemainingToPlace -= 1;
            }

            //if (dotsRemainingToPlace <= 0)
            //{
            //    break;
            //}
        }

        //Init scanline
        timeLeftCurrentScanline = timeBetweenScanLines;
        currentRow = 0;
        GameObject scanLine = Instantiate(scanLinePrefab, transform) as GameObject;
        scanlineTransform = scanLine.transform;
        Vector2 localPosition = scanlineTransform.localPosition;
        localPosition.y = currentRow;
        scanlineTransform.localPosition = localPosition;
        possibleDropId = -1;
        StartCoroutine("UpdateConnectionsOnStart");
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
        if (!pauseTimer)
        {
            timeLeftCurrentScanline -= Time.smoothDeltaTime;
        }

        if (timeLeftCurrentScanline <= 0)
        {
            MoveScanline();
            ClearDots();
            CheckIfDraggedDotIsStillThere();
        }

        Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

        //Debug.Log($"update: {name}");
        if (Input.GetMouseButtonDown(0))
        {
            // currentPickType = Lane.LaneType.Columns;
            currentPickType = Lane.LaneType.Row;


            for (int i = 0; i < hits.Length; i++)
            {
                possibleCurrentPick[i] = hits[i].collider.GetComponent<Lane>();
            }

            // find current pick
            for (int i = 0; i < possibleCurrentPick.Length; i++)
            {
                if (possibleCurrentPick[i].laneType == currentPickType)
                {
                    currentPick = possibleCurrentPick[i];
                }
            }

        }
        else if (Input.GetMouseButtonUp(0))
        {
            currentPick = null;
            currentDrop = null;
            possibleCurrentPick[0] = null;
            possibleCurrentPick[1] = null;
            possibleDropId = -1;

        }
        else if (Input.GetMouseButton(0))
        {
            // find possible drop
            for (int i = 0; i < hits.Length; i++)
            {
                Lane possibleDrop = hits[i].collider.GetComponent<Lane>();
                if (possibleDrop.laneType == currentPickType)
                {
                    currentDrop = possibleDrop;
                    //possibleDropId = currentDrop.id;
                }
            }

            if (possibleDropId != currentDrop.id)
            //if (true)
            {
                possibleDropId = currentDrop.id;

                if (currentPick.id < currentDrop.id)
                {
                    for (int j = currentDrop.id; currentPick.id < j; j--)
                    {
                        string a_5 = "";
                        string a_4 = "";
                        string a_3 = "";
                        string a_2 = "";
                        string a_1 = "";
                        string a_0 = "";

                        string b_5 = "";
                        string b_4 = "";
                        string b_3 = "";
                        string b_2 = "";
                        string b_1 = "";
                        string b_0 = "";

                        if (currentPickType == Lane.LaneType.Columns)
                        {
                            a_5 = $"5_{j}";
                            a_4 = $"4_{j}";
                            a_3 = $"3_{j}";
                            a_2 = $"2_{j}";
                            a_1 = $"1_{j}";
                            a_0 = $"0_{j}";

                            b_5 = $"5_{j - 1}";
                            b_4 = $"4_{j - 1}";
                            b_3 = $"3_{j - 1}";
                            b_2 = $"2_{j - 1}";
                            b_1 = $"1_{j - 1}";
                            b_0 = $"0_{j - 1}";
                        }
                        else
                        {
                            a_5 = $"{j}_5";
                            a_4 = $"{j}_4";
                            a_3 = $"{j}_3";
                            a_2 = $"{j}_2";
                            a_1 = $"{j}_1";
                            a_0 = $"{j}_0";

                            b_5 = $"{j - 1}_5";
                            b_4 = $"{j - 1}_4";
                            b_3 = $"{j - 1}_3";
                            b_2 = $"{j - 1}_2";
                            b_1 = $"{j - 1}_1";
                            b_0 = $"{j - 1}_0";
                        }

                        GameObject.Find(a_5).GetComponent<DotScript>().SetNewName(b_5);
                        GameObject.Find(a_4).GetComponent<DotScript>().SetNewName(b_4);
                        GameObject.Find(a_3).GetComponent<DotScript>().SetNewName(b_3);
                        GameObject.Find(a_2).GetComponent<DotScript>().SetNewName(b_2);
                        GameObject.Find(a_1).GetComponent<DotScript>().SetNewName(b_1);
                        GameObject.Find(a_0).GetComponent<DotScript>().SetNewName(b_0);
                    }
                }
                else if (currentPick.id > currentDrop.id)
                {
                    for (int j = currentDrop.id; j < currentPick.id; j++)
                    {

                        string a_5 = "";
                        string a_4 = "";
                        string a_3 = "";
                        string a_2 = "";
                        string a_1 = "";
                        string a_0 = "";

                        string b_5 = "";
                        string b_4 = "";
                        string b_3 = "";
                        string b_2 = "";
                        string b_1 = "";
                        string b_0 = "";

                        if (currentPickType == Lane.LaneType.Columns)
                        {
                            a_5 = $"5_{j}";
                            a_4 = $"4_{j}";
                            a_3 = $"3_{j}";
                            a_2 = $"2_{j}";
                            a_1 = $"1_{j}";
                            a_0 = $"0_{j}";

                            b_5 = $"5_{j + 1}";
                            b_4 = $"4_{j + 1}";
                            b_3 = $"3_{j + 1}";
                            b_2 = $"2_{j + 1}";
                            b_1 = $"1_{j + 1}";
                            b_0 = $"0_{j + 1}";
                        }
                        else
                        {
                            a_5 = $"{j}_5";
                            a_4 = $"{j}_4";
                            a_3 = $"{j}_3";
                            a_2 = $"{j}_2";
                            a_1 = $"{j}_1";
                            a_0 = $"{j}_0";

                            b_5 = $"{j + 1}_5";
                            b_4 = $"{j + 1}_4";
                            b_3 = $"{j + 1}_3";
                            b_2 = $"{j + 1}_2";
                            b_1 = $"{j + 1}_1";
                            b_0 = $"{j + 1}_0";
                        }

                        GameObject.Find(a_5).GetComponent<DotScript>().SetNewName(b_5);
                        GameObject.Find(a_4).GetComponent<DotScript>().SetNewName(b_4);
                        GameObject.Find(a_3).GetComponent<DotScript>().SetNewName(b_3);
                        GameObject.Find(a_2).GetComponent<DotScript>().SetNewName(b_2);
                        GameObject.Find(a_1).GetComponent<DotScript>().SetNewName(b_1);
                        GameObject.Find(a_0).GetComponent<DotScript>().SetNewName(b_0);
                    }
                }

                if (currentPickType == Lane.LaneType.Columns)
                {
                    GameObject.Find($"5_{currentPick.id}").GetComponent<DotScript>().SetNewName($"5_{currentDrop.id}");
                    GameObject.Find($"4_{currentPick.id}").GetComponent<DotScript>().SetNewName($"4_{currentDrop.id}");
                    GameObject.Find($"3_{currentPick.id}").GetComponent<DotScript>().SetNewName($"3_{currentDrop.id}");
                    GameObject.Find($"2_{currentPick.id}").GetComponent<DotScript>().SetNewName($"2_{currentDrop.id}");
                    GameObject.Find($"1_{currentPick.id}").GetComponent<DotScript>().SetNewName($"1_{currentDrop.id}");
                    GameObject.Find($"0_{currentPick.id}").GetComponent<DotScript>().SetNewName($"0_{currentDrop.id}");
                }
                else
                {
                    GameObject.Find($"{currentPick.id}_5").GetComponent<DotScript>().SetNewName($"{currentDrop.id}_5");
                    GameObject.Find($"{currentPick.id}_4").GetComponent<DotScript>().SetNewName($"{currentDrop.id}_4");
                    GameObject.Find($"{currentPick.id}_3").GetComponent<DotScript>().SetNewName($"{currentDrop.id}_3");
                    GameObject.Find($"{currentPick.id}_2").GetComponent<DotScript>().SetNewName($"{currentDrop.id}_2");
                    GameObject.Find($"{currentPick.id}_1").GetComponent<DotScript>().SetNewName($"{currentDrop.id}_1");
                    GameObject.Find($"{currentPick.id}_0").GetComponent<DotScript>().SetNewName($"{currentDrop.id}_0");
                }
                //DotScript[] dots = GetDotsColumn(possibleDropId);

                DotScript[] dots = GameObject.FindObjectsOfType<DotScript>();
                foreach (DotScript dot in dots)
                {
                    dot.SwapName();
                }
            }


            // update current pick
            for (int i = 0; i < hits.Length; i++)
            {
                possibleCurrentPick[i] = hits[i].collider.GetComponent<Lane>();
            }

            // find current pick
            for (int i = 0; i < possibleCurrentPick.Length; i++)
            {
                if (possibleCurrentPick[i].laneType == currentPickType)
                {
                    currentPick = possibleCurrentPick[i];
                }
            }
        }

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

    private DotScript[] GetDotsColumn(int possibleDropId)
    {

        List<DotScript> res = new List<DotScript>();
        if (possibleDropId == 0) return res.ToArray();

        DotScript[] dots = GameObject.FindObjectsOfType<DotScript>();
        foreach(DotScript dot in dots)
        {
            if (dot.name[2].ToString() == possibleDropId.ToString())
            {
                res.Add(dot);
            }
        }

        return res.ToArray();
    }

    public void MoveLane(Lane pick, Lane drop)
    {
        //DotScript.Type type5 = GameObject.Find($"5_{pick.id}").GetComponent<DotScript>().color;
        //DotScript.Type type4 = GameObject.Find($"4_{pick.id}").GetComponent<DotScript>().color;
        //DotScript.Type type3 = GameObject.Find($"3_{pick.id}").GetComponent<DotScript>().color;
        //DotScript.Type type2 = GameObject.Find($"2_{pick.id}").GetComponent<DotScript>().color;
        //DotScript.Type type1 = GameObject.Find($"1_{pick.id}").GetComponent<DotScript>().color;
        //DotScript.Type type0 = GameObject.Find($"0_{pick.id}").GetComponent<DotScript>().color;

        if (pick.id < drop.id)
        {
            for (int i = drop.id; pick.id < i; i--)
            {
                GameObject.Find($"5_{i}").GetComponent<DotScript>().SetNewName($"5_{i - 1}");
                GameObject.Find($"4_{i}").GetComponent<DotScript>().SetNewName($"4_{i - 1}");
                GameObject.Find($"3_{i}").GetComponent<DotScript>().SetNewName($"3_{i - 1}");
                GameObject.Find($"2_{i}").GetComponent<DotScript>().SetNewName($"2_{i - 1}");
                GameObject.Find($"1_{i}").GetComponent<DotScript>().SetNewName($"1_{i - 1}");
                GameObject.Find($"0_{i}").GetComponent<DotScript>().SetNewName($"0_{i - 1}");
            }
        }

        GameObject.Find($"5_{pick.id}").GetComponent<DotScript>().SetNewName($"5_{drop.id}");
        GameObject.Find($"4_{pick.id}").GetComponent<DotScript>().SetNewName($"4_{drop.id}");
        GameObject.Find($"3_{pick.id}").GetComponent<DotScript>().SetNewName($"3_{drop.id}");
        GameObject.Find($"2_{pick.id}").GetComponent<DotScript>().SetNewName($"2_{drop.id}");
        GameObject.Find($"1_{pick.id}").GetComponent<DotScript>().SetNewName($"1_{drop.id}");
        GameObject.Find($"0_{pick.id}").GetComponent<DotScript>().SetNewName($"0_{drop.id}");

        DotScript[] dots = GameObject.FindObjectsOfType<DotScript>();
        foreach(DotScript dot in dots)
        {
            dot.SwapName();
        }
        //Debug.Log("");
    }

    private void MoveScanline()
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

        DropNewDots();
    }

    private void ClearDots()
    {
        int row = currentRow;

        DotScript test5 = GameObject.Find($"{row}_5").GetComponent<DotScript>();
        DotScript test4 = GameObject.Find($"{row}_4").GetComponent<DotScript>();
        DotScript test3 = GameObject.Find($"{row}_3").GetComponent<DotScript>();
        DotScript test2 = GameObject.Find($"{row}_2").GetComponent<DotScript>();
        DotScript test1 = GameObject.Find($"{row}_1").GetComponent<DotScript>();
        DotScript test0 = GameObject.Find($"{row}_0").GetComponent<DotScript>();

        DotScript[] dotsToClear = { test5, test4, test3, test2, test1, test0 };

        foreach(DotScript dot in dotsToClear)
        {
            if (dot.connectedTo)
            {
                dot.SetType(DotScript.Type.Empty);
                dot.connectedTo.SetType(DotScript.Type.Empty);

                dot.highlight.gameObject.SetActive(false);
                dot.connectedTo.highlight.gameObject.SetActive(false);

                dot.connectedTo = null;
            }
        }
    }

    private void CheckIfDraggedDotIsStillThere()
    {
        if ( draggedDot )
        {
            if ( draggedDot.activeInHierarchy )
            {
                draggedDot = null;
                isDragging = false;
                crosshair.SetActive(false);
            }
        }
    }

    void UpdateConnections()
    {
        //Debug.Log("UpdateConnection");
        // Reset all connections
        for (int i = 0; i < transform.childCount; i++)
        {
            DotScript dot = transform.GetChild(i).GetComponent<DotScript>();
            if (dot)
            {
                dot.connectedTo = null;
                dot.highlight.gameObject.SetActive(false);
                if (dot.connectedTo) dot.connectedTo.highlight.gameObject.SetActive(false);
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

                        child.highlight.gameObject.SetActive(true);
                        child.connectedTo.highlight.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    void DropNewDots()
    {
        int leftToDropCount = maxDrop;
        for (int i = 0; i < transform.childCount; i++)
        {
            if (leftToDropCount <= 0)
            {
                break;
            }

            if (Random.Range(0.0f, 1.0f) >= .8f)
            {
                DotScript dot = transform.GetChild(i).GetComponent<DotScript>();

                if (dot && dot.color == DotScript.Type.Empty)
                {
                    dot.SetType(DotScript.GetRandomColor());
                    leftToDropCount -= 1;
                }
            }
        }
        UpdateConnections();
    }

    void OnGUI()
    {

        GUI.Label(new Rect(10, 0, 1000, 90), $"Time: {timeLeftCurrentScanline}");
        if (pick1) GUI.Label(new Rect(10, 16, 1000, 90), pick1.name);
        if (pick2) GUI.Label(new Rect(10, 26, 1000, 90), pick2.name);
        if (isDragging) GUI.Label(new Rect(10, 36, 1000, 90), $"DRAGGING: {draggedDot.name}");
        if (currentPick) GUI.Label(new Rect(10, 46, 1000, 90), "Current Pick: " + currentPick.id);
        if (possibleDropId != -1) GUI.Label(new Rect(10, 56, 1000, 90), "Possible drop: " + possibleDropId);

        if (GUI.Button(new Rect(10, 86, 100, 20), "Spawn Dots"))
        {
            DropNewDots();
        }

        //Debug buttons
        //string[] topRow = { "5_0", "5_1", "5_2", "5_3", "5_4", "5_5" };
        //foreach(string row in topRow)
        //{
        //    GameObject test = GameObject.Find(row);
        //    Vector2 position = Camera.main.WorldToScreenPoint(test.transform.position);
        //    if (GUI.Button(new Rect(position.x, Screen.height - position.y - 40, 20, 20), "U"))
        //    {
        //        MoveDots(row, "U");
        //        UpdateConnections();
        //    }
        //}

        //string[] bottomRow = { "0_0", "0_1", "0_2", "0_3", "0_4", "0_5" };
        //foreach (string row in bottomRow)
        //{
        //    GameObject test = GameObject.Find(row);
        //    Vector2 position = Camera.main.WorldToScreenPoint(test.transform.position);
        //    if (GUI.Button(new Rect(position.x, Screen.height - position.y + 20, 20, 20), "D"))
        //    {
        //        MoveDots(row, "D");
        //        UpdateConnections();
        //    }
        //}

        //string[] leftRow = { "0_0", "1_0", "2_0", "3_0", "4_0", "5_0" };
        //foreach (string row in leftRow)
        //{
        //    GameObject test = GameObject.Find(row);
        //    Vector2 position = Camera.main.WorldToScreenPoint(test.transform.position);
        //    if (GUI.Button(new Rect(position.x - 40, Screen.height - position.y, 20, 20), "L"))
        //    {
        //        MoveDots(row, "L");
        //        UpdateConnections();
        //    }
        //}

        //string[] rightRow = { "0_5", "1_5", "2_5", "3_5", "4_5", "5_5" };
        //foreach (string row in rightRow)
        //{
        //    GameObject test = GameObject.Find(row);
        //    Vector2 position = Camera.main.WorldToScreenPoint(test.transform.position);
        //    if (GUI.Button(new Rect(position.x + 20, Screen.height - position.y, 20, 20), "R"))
        //    {
        //        MoveDots(row, "R");
        //        UpdateConnections();
        //    }
        //}
    }
}
