using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotScript : MonoBehaviour
{
    public FieldScript field;
    public Color[] loller = new Color[] { Color.red, Color.green, Color.blue };
    public int currentRow;
    public int currentColumn;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = loller[Random.Range(0, 3)];        
    }

    // Update is called once per frame
    void Update()
    {
        name = $"{currentRow}_{currentColumn}";
    }

    private void OnGUI()
    {
        GUI.backgroundColor = Color.yellow;
        Vector2 loller = Camera.main.WorldToScreenPoint(transform.position);
        GUI.Label(new Rect(loller.x, Screen.height - loller.y, 100, 100), name);
    }
}
