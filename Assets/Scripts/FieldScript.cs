using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FieldScript : MonoBehaviour
{
    public Object prefab;
    public Transform dotsContainer;
    public int rows;
    public int columns;
    public float spacing;

    public DotScript pick1;
    public DotScript pick2;

    //Crosshair
    public GameObject crosshair;
    bool isDragging;
    GameObject draggedDot;

    public int maxDrop;

    // Prototype 2
    public Lane currentPick;
    public Lane[] possibleCurrentPick;
    public Lane.LaneType currentPickType;
    //public bool pauseTimer;
    public int possibleDropId;
    public Lane currentDrop;

    public bool    bMouseCoordsOnClick;
    public Vector2 vMouseCoordsOnClick;
    public bool    bMouseCoordsNow;
    public Vector2 vMouseCoordsNow;
    public bool    bCurrentPickTypeLocked;
    public int     noConnectionRequired;
    public int     score;
    private bool   bGameOver;

    // UI
    public Text scoreText;
    //public Image timerImage;

    //Refactor
    DotScript[] dotsArray;

    private void OnEnable()
    {
        EventManager.OnTimerEnd += ClearDots;
        EventManager.OnTimerEnd += DropNewDots;

        EventManager.OnTouchStart += OnTouchStart;
        EventManager.OnTouchMove  += OnTouchMove;
        EventManager.OnTouchEnd   += OnTouchEnd;

        EventManager.OnGameOver += OnGameOver;
    }

    private void OnDisable()
    {
        EventManager.OnTimerEnd -= ClearDots;
        EventManager.OnTimerEnd -= DropNewDots;

        EventManager.OnTouchStart -= OnTouchStart;
        EventManager.OnTouchMove  -= OnTouchMove;
        EventManager.OnTouchEnd   -= OnTouchEnd;

        EventManager.OnGameOver -= OnGameOver;
    }

    void Start()
    {
        Setup();
    }

    void Update()
    {
        if (bGameOver)
        {
            return;
        }

        UpdateUI();
    }

    private void Setup()
    {
        dotsArray = new DotScript[rows * columns];
        possibleCurrentPick = new Lane[2];

        for (int i = 0; i < rows * columns; i++)
        {
            var x = i % rows;
            var y = i / columns;
            GameObject child = Instantiate(prefab, Vector3.zero, Quaternion.identity, dotsContainer) as GameObject;
            Vector2 position = new Vector2(x, y);
            child.transform.localPosition = position;
            DotScript script = child.GetComponent<DotScript>();
            script.field = this;
            script.currentX = x;
            script.currentY = y;
            script.SetType(DotScript.Type.Empty);
            dotsArray[i] = script;
        }

        possibleDropId = -1;
    }

    void OnTouchStart()
    {
        if (bGameOver)
        {
            return;
        }

        Vector2 pos = InputScript.GetTouchPosition();
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

        for (int i = 0; i < hits.Length; i++)
        {
            possibleCurrentPick[i] = hits[i].collider.GetComponent<Lane>();
        }

        bMouseCoordsOnClick = true;
        vMouseCoordsOnClick = pos;

        bMouseCoordsNow = true;
        vMouseCoordsNow = pos;

        currentPick = null;
    }

    void OnTouchMove()
    {
        if (bGameOver)
        {
            return;
        }

        Vector2 pos = InputScript.GetTouchPosition();
        RaycastHit2D[] hits = Physics2D.RaycastAll(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

        bMouseCoordsNow = true;
        vMouseCoordsNow = pos;

        float dist = Vector3.Distance(vMouseCoordsNow, vMouseCoordsOnClick);
        float dotProduct = Vector3.Dot(Vector3.right, (vMouseCoordsNow - vMouseCoordsOnClick).normalized);

        if (!bCurrentPickTypeLocked && dist >= 5.0f)
        {
            float absDotProduct = Mathf.Abs(dotProduct);
            if (absDotProduct >= 0.5)
            {
                currentPickType = Lane.LaneType.Columns;
            }
            else
            {
                currentPickType = Lane.LaneType.Row;
            }

            //find current pick
            for (int i = 0; i < possibleCurrentPick.Length; i++)
            {
                if (possibleCurrentPick[i].laneType == currentPickType)
                {
                    currentPick = possibleCurrentPick[i];
                }
            }

            bCurrentPickTypeLocked = true;

        }

        if (!currentPick)
        {
            return;
        }

        // find possible drop
        for (int i = 0; i < hits.Length; i++)
        {
            Lane possibleDrop = hits[i].collider.GetComponent<Lane>();
            if (possibleDrop.laneType == currentPickType)
            {
                currentDrop = possibleDrop;
            }
        }

        if (possibleDropId != currentDrop.id)
        {
            possibleDropId = currentDrop.id;

            if (currentPick.id < currentDrop.id)
            {
                for (int j = currentDrop.id; currentPick.id < j; j--)
                {
                    if (currentPickType == Lane.LaneType.Columns)
                    {
                        MoveColumn(j, j - 1);
                    }
                    else
                    {
                        MoveRow(j, j - 1);
                    }
                }
            }
            else if (currentPick.id > currentDrop.id)
            {
                for (int j = currentDrop.id; j < currentPick.id; j++)
                {
                    if (currentPickType == Lane.LaneType.Columns)
                    {
                        MoveColumn(j, j + 1);
                    }
                    else
                    {
                        MoveRow(j, j + 1);
                    }
                }
            }

            if (currentPickType == Lane.LaneType.Columns)
            {
                MoveColumn(currentPick.id, currentDrop.id);
            }
            else
            {
                MoveRow(currentPick.id, currentDrop.id);
            }

            DotScript[] dots = GameObject.FindObjectsOfType<DotScript>();
            foreach (DotScript dot in dots)
            {
                dot.SwapName();
            }

            UpdateConnections();
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

    void OnTouchEnd()
    {
        if (bGameOver)
        {
            return;
        }

        currentPick = null;
        currentDrop = null;
        possibleCurrentPick[0] = null;
        possibleCurrentPick[1] = null;
        possibleDropId = -1;

        bMouseCoordsNow = bMouseCoordsOnClick = bCurrentPickTypeLocked = false;
    }

    private void UpdateUI()
    {
        scoreText.text = score.ToString(); //TODO: this should be handled by the gameplay manager
        //timerImage.fillAmount = timeLeftCurrentScanline; TODO: Moved to TimerScript
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

    private void MoveColumn(int j, int v)
    {
        for (int i = 0; i < columns; i++)
        {
            MoveDot(GetDotAtXY(j, i), v, i);
        }
    }

    private void MoveRow(int j, int v)
    {
        for (int i = 0; i < columns; i++)
        {
            MoveDot(GetDotAtXY(i, j), i, v);
        }
    }

    private void MoveDot(DotScript dotScript, int x, int y)
    {
        dotScript.SetNewName($"{x}_{y}");
    }

    private void ClearDots()
    {
        DotScript[] dotsToClear = GameObject.FindObjectsOfType<DotScript>();

        foreach (DotScript dot in dotsToClear)
        {
            List<DotScript> connectedDots = new List<DotScript>();
            if (dot.connectedTo)
            {
                connectedDots.Add(dot);
                DotScript connectedTo = dot.connectedTo;
                while (connectedTo)
                {
                    connectedDots.Add(connectedTo);
                    connectedTo = connectedTo.connectedTo;
                }
            }

            if (connectedDots.Count >= noConnectionRequired)
            {
                foreach(DotScript d in connectedDots)
                {
                    d.SetType(DotScript.Type.Empty);
                    d.highlight.gameObject.SetActive(false);
                    d.connectedTo = null;

                    score += 1;
                }
            }
        }
    }

    void UpdateConnections()
    {
        // Reset all connections
        foreach (var dot in dotsArray)
        {
                dot.connectedTo = null;
                dot.highlight.gameObject.SetActive(false);
                if (dot.connectedTo) dot.connectedTo.highlight.gameObject.SetActive(false);
        }

        // Check for new connections
        for (int j = 0; j < columns - 1; j++)
        {
            for (int i = 0; i < rows; i++)
            {
                DotScript dot = GetDotAtXY(j, i);

                if (dot.color != DotScript.Type.Empty)
                {
                    //DotScript neighbour = GameObject.Find($"{i}_{j + 1}").GetComponent<DotScript>();
                    DotScript neighbour = GetDotAtXY(j + 1, i);

                    if (dot.color == neighbour.color)
                    {
                        dot.connectedTo = neighbour;

                        dot.highlight.gameObject.SetActive(true);
                        dot.connectedTo.highlight.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    void UpdateConnectionsOld()
    {
        // Reset all connections
        for (int i = 0; i < dotsContainer.childCount; i++)
        {
            DotScript dot = dotsContainer.GetChild(i).GetComponent<DotScript>();
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
        if (CountDots() >= 36)
        {
            EventManager.OnGameOver();
        }

        int leftToDropCount = maxDrop;
        for (int i = 0; i < dotsContainer.childCount; i++)
        {
            if (leftToDropCount <= 0)
            {
                break;
            }

            if (Random.Range(0.0f, 1.0f) >= .8f)
            {
                DotScript dot = dotsContainer.GetChild(i).GetComponent<DotScript>();

                if (dot && dot.color == DotScript.Type.Empty)
                {
                    var possibleColors = new List<DotScript.Type>();
                    possibleColors.Add(DotScript.Type.Red);
                    possibleColors.Add(DotScript.Type.Gree);
                    possibleColors.Add(DotScript.Type.Blue);

                    //Check left
                    int currC = dot.currentY;
                    int currR = dot.currentX;

                    DotScript leftDot = GetDotByCoords(currR, currC - 1);
                    if (leftDot && leftDot.color != DotScript.Type.Empty)
                    {
                        possibleColors.Remove(leftDot.color);
                    }

                    DotScript rightDot = GetDotByCoords(currR, currC + 1);
                    if (rightDot && rightDot.color != DotScript.Type.Empty)
                    {
                        possibleColors.Remove(rightDot.color);
                    }

                    dot.SetType(DotScript.GetRandomColor(possibleColors));
                    leftToDropCount -= 1;
                }
            }
        }
        UpdateConnections();
    }

    private DotScript GetDotByCoords(int r, int c)
    {
        var go = GameObject.Find($"{r}_{c}");
        if (go)
        {
            return go.GetComponent<DotScript>();
        }
        else
        {
            return null;
        }
    }

    private DotScript GetDotAtXY(int x, int y)
    {
        foreach(var dot in dotsArray)
        {
            if (dot.currentX == x && dot.currentY == y)
            {
                return dot;
            }
        }

        return null;
    }

    private int CountDots(bool onlyEmpty = false)
    {
        var res = 0;
        foreach (var number in GameObject.FindObjectsOfType<DotScript>())
        {
            if (onlyEmpty)
            {
                if (number.color == DotScript.Type.Empty) res += 1;
            }
            else
            {
                if(number.color != DotScript.Type.Empty)  res += 1;
            }
        }

        return res;
    }

    private int Score() //TODO: this should be handled by the gameplay manager
    {
        return score;
    }

    void OnGUI()
    {
        //GUI.Label(new Rect(10, 0, 1000, 90), $"Time: {timeLeftCurrentScanline}"); TODO: Moved to TimerScript
        if (pick1) GUI.Label(new Rect(10, 16, 1000, 90), pick1.name);
        if (pick2) GUI.Label(new Rect(10, 26, 1000, 90), pick2.name);
        if (isDragging) GUI.Label(new Rect(10, 36, 1000, 90), $"DRAGGING: {draggedDot.name}");
        if (currentPick) GUI.Label(new Rect(10, 46, 1000, 90), "Current Pick: " + currentPick.id);
        if (currentPick) GUI.Label(new Rect(10, 56, 1000, 90), "Current Type: " + currentPickType.ToString());
        if (possibleDropId != -1) GUI.Label(new Rect(10, 66, 1000, 90), "Possible drop: " + possibleDropId);
        GUI.Label(new Rect(10, 77, 1000, 90), "Dots       : " + CountDots()     + "/36");
        GUI.Label(new Rect(10, 87, 1000, 90), "Dots(empty): " + CountDots(true) + "/36");
        GUI.Label(new Rect(10, 97, 1000, 90), "Score: " + Score());
        if (bMouseCoordsOnClick) GUI.Label(new Rect(10, 106, 1000, 90), "Mouse onClick: " + vMouseCoordsOnClick.ToString());
        if (bMouseCoordsNow) GUI.Label(new Rect(10, 116, 1000, 90), "Mouse now: " + vMouseCoordsNow.ToString());
        if (bGameOver) GUI.Label(new Rect(10, 126, 1000, 90), "GAME OVER");

        if (GUI.Button(new Rect(10, 156, 100, 20), "Spawn Dots"))
        {
            DropNewDots();
        }

        if (GUI.Button(new Rect(10, 176, 100, 20), "Reset Game"))
        {
            ResetGame();
        }
    }

    private void ResetGame()
    {
        SceneManager.LoadScene(0); //TODO: this should be handled by the gameplay manager
    }

    private void OnGameOver()
    {
        bGameOver = true; //TODO: this should be handled by the gameplay manager
    }
}
