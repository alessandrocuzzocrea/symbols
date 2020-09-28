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

    // Start is called before the first frame update
    void Start()
    {
        //GetComponent<SpriteRenderer>().color = loller[Random.Range(0, 3)];        
    }

    // Update is called once per frame
    void Update()
    {
        name = $"{currentRow}_{currentColumn}";
    }

    public void SetType(Type c)
    {
        color = c;
        GetComponent<SpriteRenderer>().color = loller[(int) color];
    }

    private void OnGUI()
    {
        GUI.backgroundColor = Color.yellow;
        Vector2 loller = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Label(new Rect(loller.x, Screen.height - loller.y, 100, 100), name);
    }
}
