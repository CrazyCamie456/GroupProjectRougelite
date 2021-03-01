using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class SoundTest : MonoBehaviour
{
    float timer = 0;
    public AK.Wwise.Event wiseevent;
    // Start is called before the first frame update
    void OnEnable()
    {
        wiseevent.Post(gameObject);
    }

    private void OnDisable()
    {
        wiseevent.Stop(gameObject);
    }
}