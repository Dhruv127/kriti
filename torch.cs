using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// using UnityEngine.Experimental.Rendering.Universal;

public class torch : MonoBehaviour
{
    // Start is called before the first frame update
    Light light;
    void Start()
    {
        light=GetComponent<Light>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyUp(KeyCode.B))
        {
            light.enabled=!light.enabled;
        }
    }
}
