using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FieldScript : MonoBehaviour
{
    public Object prefab;
    public int rows;
    public int columns;
    public float spacing;

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
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log($"update: {name}");
        if (Input.GetMouseButtonDown(0))
        {
            Vector2 pos = new Vector2(Input.mousePosition.x, Input.mousePosition.y);
            RaycastHit2D hit = Physics2D.Raycast(Camera.main.ScreenToWorldPoint(pos), Vector2.zero);

            if (hit)
            {
                Debug.Log(hit.transform.gameObject.name);
            }
        }
    }
}
