using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DotScript : MonoBehaviour
{
    public FieldScript field;
    public Color[] loller = new Color[] { Color.red, Color.green, Color.blue };
    
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<SpriteRenderer>().color = loller[Random.Range(0, 3)];        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"update: {name}");
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

            if(hit)
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }
    }
}
