using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.Rendering.Universal;

public class LightFade : MonoBehaviour
{
    public Light2D lt;
    float t;

    // Start is called before the first frame update
    void Start()
    {
        t = Time.time;
        lt = GetComponent<Light2D>();
    }
    
    // Update is called once per frame
    void Update()
    {
        LightFader();
    }

    void LightFader()
    {
        lt.intensity = Mathf.Lerp(0.01f, 1, (Time.time - t) /10);
    }
}
