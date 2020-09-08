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
                Instantiate(prefab, new Vector3(j * spacing, i * spacing, 0), Quaternion.identity, transform);
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
