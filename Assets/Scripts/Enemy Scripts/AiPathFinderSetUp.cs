using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;
public class AiPathFinderSetUp : MonoBehaviour
{
    // Start is called before the first frame update
    void Update()
    {
        GetComponent<AIDestinationSetter>().target = GameObject.Find("Player").transform;
    }


}
