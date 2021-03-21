using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWalkingSoundManager : MonoBehaviour
{
    public AK.Wwise.Event wiseevent;
    public bool isWalking;
    private float timeBetweenSteps = 0.333333f;
    private float timer = 0.0f;

    void LateUpdate()
    {
        if (isWalking && timer > timeBetweenSteps)
        {
            wiseevent.Post(gameObject);
            timer = 0.0f;
        }

        timer += Time.deltaTime;
    }
}
