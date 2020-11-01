using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FieldScript : MonoBehaviour
{
    [SerializeField]
    public GameObject DotPrefab;

    [SerializeField]
    public Transform dotsContainer;

    [SerializeField]
    private int rows;

    [SerializeField]
    private int columns;

    [SerializeField]
    private int maxDrop;

    [SerializeField]
    private int noConnectionRequired;

    private DotScript[]   dots;
    private Lane          currentPick;
    private Lane[]        possibleCurrentPick;
    private Lane.LaneType currentPickType;

    private int  possibleDropId;
    private Lane currentDrop;

    private bool    bMouseCoordsOnClick;
    private Vector2 vMouseCoordsOnClick;
    private bool    bMouseCoordsNow;
    private Vector2 vMouseCoordsNow;
    private bool    bCurrentPickTypeLocked;

    //Dependencies
    private GameplayScript gameplayScript;

    private void OnEnable()
    {
        EventManager.OnTimerEnd += ClearDots;
        EventManager.OnTimerEnd += DropNewDots;

        EventManager.OnTouchStart += OnTouchStart;
        EventManager.OnTouchMove  += OnTouchMove;
        EventManager.OnTouchEnd   += OnTouchEnd;
    }

    private void OnDisable()
    {
        EventManager.OnTimerEnd -= ClearDots;
        EventManager.OnTimerEnd -= DropNewDots;

        EventManager.OnTouchStart -= OnTouchStart;
        EventManager.OnTouchMove  -= OnTouchMove;
        EventManager.OnTouchEnd   -= OnTouchEnd;
    }

    void Start()
    {
        GetDependencies();
        SetupDots();
    }

    private void GetDependencies()
    {
        gameplayScript = GameObject.FindObjectOfType<GameplayScript>();
    }

    private void SetupDots()
    {
        dots = new DotScript[rows * columns];
        possibleCurrentPick = new Lane[2];

        for (int i = 0; i < rows * columns; i++)
        {
            int x = i % rows;
            int y = i / columns;

            GameObject child = Instantiate(DotPrefab, Vector3.zero, Quaternion.identity, dotsContainer) as GameObject;
            DotScript dot = child.GetComponent<DotScript>();
            dot.Init(x, y);
            dots[i] = dot;
        }

        possibleDropId = -1;
    }

    void OnTouchStart()
    {
        if (gameplayScript.IsGameOver)
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
        if (gameplayScript.IsGameOver)
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
        if (gameplayScript.IsGameOver)
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
        foreach (DotScript dot in dots)
        {
            if (dot.leftConnectedTo || dot.upConnectedTo)
            {
                dot.SetType(DotScript.Type.Empty);
                dot.highlight.gameObject.SetActive(false);


                DotScript left = dot.leftConnectedTo;
                if (left)
                {
                    left.SetType(DotScript.Type.Empty);
                    left.highlight.gameObject.SetActive(false);
                }

                DotScript up = dot.upConnectedTo;
                if (up)
                {
                    up.SetType(DotScript.Type.Empty);
                    up.highlight.gameObject.SetActive(false);
                }

                dot.leftConnectedTo = dot.upConnectedTo = null;

                EventManager.OnIncreaseScore();
            }
        }
    }

    private void ClearDotsOld()
    {
        foreach (DotScript dot in dots)
        {
            List<DotScript> connectedDots = new List<DotScript>();
            if (dot.leftConnectedTo)
            {
                connectedDots.Add(dot);
                DotScript connectedTo = dot.leftConnectedTo;
                while (connectedTo)
                {
                    connectedDots.Add(connectedTo);
                    connectedTo = connectedTo.leftConnectedTo;
                }
            }

            if (connectedDots.Count >= noConnectionRequired)
            {
                foreach (DotScript d in connectedDots)
                {
                    d.SetType(DotScript.Type.Empty);
                    d.highlight.gameObject.SetActive(false);
                    d.leftConnectedTo = null;

                    EventManager.OnIncreaseScore();
                }
            }
        }
    }

    void UpdateConnections()
    {
        // Reset all connections
        foreach (var dot in dots)
        {
            dot.leftConnectedTo = dot.upConnectedTo = null;
            dot.highlight.gameObject.SetActive(false);
            if (dot.leftConnectedTo) dot.leftConnectedTo.highlight.gameObject.SetActive(false);
            if (dot.upConnectedTo) dot.upConnectedTo.highlight.gameObject.SetActive(false);
        }

        // Check for new connections
        for (int j = 0; j < columns; j++)
        {
            for (int i = 0; i < rows; i++)
            {
                DotScript dot = GetDotAtXY(j, i);

                if (dot && dot.color != DotScript.Type.Empty)
                {
                    // Left neighbour
                    DotScript leftNeighbour = GetDotAtXY(j + 1, i);
                    if (leftNeighbour && dot.color == leftNeighbour.color)
                    {
                        dot.highlight.gameObject.SetActive(true);
                        dot.leftConnectedTo = leftNeighbour;
                        dot.leftConnectedTo.highlight.gameObject.SetActive(true);
                    }

                    // Upper neighbour
                    DotScript upNeighbour = GetDotAtXY(j, i + 1);
                    if (upNeighbour && dot.color == upNeighbour.color)
                    {
                        dot.highlight.gameObject.SetActive(true);
                        dot.upConnectedTo = upNeighbour;
                        dot.upConnectedTo.highlight.gameObject.SetActive(true);
                    }
                }
            }
        }
    }

    void DropNewDots()
    {
        if (CountDots() >= columns * rows)
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
                    int currX = dot.currentX;
                    int currY = dot.currentY;

                    DotScript leftDot = GetDotAtXY(currX - 1, currY);
                    if (leftDot && leftDot.color != DotScript.Type.Empty)
                    {
                        possibleColors.Remove(leftDot.color);
                    }

                    DotScript rightDot = GetDotAtXY(currX + 1, currY);
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

    private DotScript GetDotAtXY(int x, int y)
    {
        foreach(var dot in dots)
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
        foreach (var dot in dots)
        {
            if (onlyEmpty)
            {
                if (dot.color == DotScript.Type.Empty) res += 1;
            }
            else
            {
                if(dot.color != DotScript.Type.Empty)  res += 1;
            }
        }

        return res;
    }

#if DEVELOPMENT_BUILD || UNITY_EDITOR
    public Lane DebugCurrentPick()
    {
        return currentPick;
    }

    public int DebugPossibleDropId()
    {
        return possibleDropId;
    }

    public int DebugCountDots(bool b = false)
    {
        return CountDots(b);
    }

    public int DebugColumns()
    {
        return columns;
    }

    public int DebugRows()
    {
        return rows;
    }

    public bool DebugbMouseCoordsOnClick()
    {
        return bMouseCoordsOnClick;
    }

    public Vector2 DebugvMouseCoordsOnClick()
    {
        return vMouseCoordsOnClick;
    }

    public bool DebugbMouseCoordsNow()
    {
        return bMouseCoordsNow;
    }

    public Vector2 DebugvMouseCoordsNow()
    {
        return vMouseCoordsNow;
    }

    public void DebugDropNewDots()
    {
        DropNewDots();
    }
#endif
}
