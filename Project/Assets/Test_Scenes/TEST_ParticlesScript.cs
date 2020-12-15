using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TEST_ParticlesScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        var pa = GetComponent<ParticleSystem>();
        var col = pa.colorOverLifetime;

        Gradient grad = new Gradient();
        grad.SetKeys(
            new GradientColorKey[] {
                        new GradientColorKey(Color.blue, 0.0f),
                        new GradientColorKey(Color.red, 1.0f)
            },
            new GradientAlphaKey[] {
                        new GradientAlphaKey(1.0f, 0.0f),
                        new GradientAlphaKey(0.0f, 1.0f)
            }
        );

        col.color = new ParticleSystem.MinMaxGradient(grad);


        Debug.Log(col);
    }
}
