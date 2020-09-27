using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScript : MonoBehaviour
{
    public Object prefab;
    public int rows;
    public int columns;
    public float spacing;

    public DotScript pick1;
    public DotScript pick2;

    public float timeBetweenScanLines = 10.0f;
    public float timeLeftCurrentScanline;

    // Start is called before the first frame update
    void Start()
    {
        for (int j = 0; j < columns; j++)
        {
            for (int i = 0; i < rows; i++)
            {
                GameObject child = Instantiate(prefab, Vector3.zero, Quaternion.identity, transform) as GameObject;
                child.transform.localPosition = new Vector3(j * 1, i * 1);
                child.name = $"Dot_{j}_{i}";
                DotScript script = child.GetComponent<DotScript>();
                script.field = this;
            }
        }

        //Init timer
        timeLeftCurrentScanline = timeBetweenScanLines;
    }

    // Update is called once per frame
    void Update()
    {
        //Update timer
        timeLeftCurrentScanline -= Time.smoothDeltaTime;
        if (timeLeftCurrentScanline <= 0)
        {
            timeLeftCurrentScanline = timeBetweenScanLines;
        }

        //Debug.Log($"update: {name}");
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

            if (hit)
            {
                Debug.Log(hit.transform.gameObject.name);

                if (pick1 == hit.transform.gameObject.GetComponent<DotScript>() || pick2 == hit.transform.gameObject.GetComponent<DotScript>())
                {
                    Debug.Log("Already picked");
                    return;
                }

                if ( pick1 == null )
                {
                    pick1 = hit.transform.gameObject.GetComponent<DotScript>();
                    return;
                }

                if ( pick2 == null )
                {
                    pick2 = hit.transform.gameObject.GetComponent<DotScript>();
                    //return;
                }

                if (pick1 && pick2) { 
                    Color color1 = pick1.GetComponent<SpriteRenderer>().color;
                    Color color2 = pick2.GetComponent<SpriteRenderer>().color;

                    pick1.GetComponent<SpriteRenderer>().color = color2;
                    pick2.GetComponent<SpriteRenderer>().color = color1;

                    pick1 = null;
                    pick2 = null;
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

    void OnGUI()
    {

        GUI.Label(new Rect(10, 0, 1000, 90), $"Time: {timeLeftCurrentScanline}");
        if (pick1) GUI.Label(new Rect(10, 16, 1000, 90), pick1.name);
        if (pick2) GUI.Label(new Rect(10, 26, 1000, 90), pick2.name);
    }
}
