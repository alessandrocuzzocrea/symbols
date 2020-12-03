using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComboLabelScript : MonoBehaviour
{
    public AnimationCurve test;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var pos = transform.position;
        pos.y = pos.y + Time.smoothDeltaTime;
        transform.position = pos;
    }
}
