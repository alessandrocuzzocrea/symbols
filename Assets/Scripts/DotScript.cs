using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotScript : MonoBehaviour
{
    public FieldScript field;
    Color[] loller = new Color[] { new Color(0, 0, 0, .1f), Color.red, Color.green, Color.blue };
    public int currentRow;
    public int currentColumn;
    public enum Type { Empty, Red, Gree, Blue }
    public Type color;
    public DotScript connectedTo;
    public GameObject sprite;
    public GameObject highlight;
    public string newName;

    public int newCurrentRow;
    public int newCurrentColumn;
    public bool isMoving;

    public float speed = 0.05f;

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<SpriteRenderer>().color = loller[Random.Range(0, 3)];
        isMoving = false;
        name = $"{currentRow}_{currentColumn}";
    }

    // Update is called once per frame
    void Update()
    {
        if (isMoving)
        {
            float step = speed * Time.deltaTime;

            // move sprite towards the target location
            transform.localPosition = Vector2.MoveTowards(transform.localPosition, new Vector3(newCurrentColumn, newCurrentRow), .2f);

            if (transform.localPosition == new Vector3(newCurrentColumn, newCurrentRow))
            {
                isMoving = false;
                currentRow    = newCurrentRow;
                currentColumn = newCurrentColumn;
            }
        }

    }

    public void SetType(Type c)
    {
        color = c;
        sprite.GetComponent<SpriteRenderer>().color = loller[(int) color];
    }

    private void OnGUI()
    {
        GUI.backgroundColor = Color.yellow;
        Vector2 loller = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Label(new Rect(loller.x, Screen.height - loller.y, 100, 100), name);
        if (connectedTo) GUI.Label(new Rect(loller.x, Screen.height - loller.y - 10, 100, 100), "C:" + connectedTo.name);
    }

    public static DotScript.Type GetRandomColor()
    {
        return (DotScript.Type) Random.Range(0, 4);
    }

    public void SetNewName(string s)
    {
        newName = s;
    }

    public void SwapName()
    {
        SwapName(null);
    }

    public void SwapName(string neewName)
    {
        if (string.IsNullOrEmpty(neewName) == false)
        {
            newName = neewName;
        }

        if (string.IsNullOrEmpty(newName))
        {
            return;
        }

        name = newName;
        newName = null;

        int newX = System.Convert.ToInt32(name[2].ToString());
        int newY = System.Convert.ToInt32(name[0].ToString());

        newCurrentRow = newY;
        newCurrentColumn = newX;

        isMoving = true;

        name = $"{newCurrentRow}_{newCurrentColumn}";


        //transform.localPosition = new Vector3(newX, newY, 0);
    }
}
