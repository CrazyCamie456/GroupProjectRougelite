using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyRangedAiPathHelper : MonoBehaviour
{

    GameObject player;
    private AIPath aiPath;

    public bool canFire = false;
    public float range;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Player");
        aiPath = GetComponent<AIPath>();
    }


    // Update is called once per frame
    void Update()
    {
        Vector3 start = transform.position;
        Vector3 destination = player.transform.position;
        Vector3 direction = destination - start;
        Vector3 directionNormalised = direction.normalized;
        float directionMagnitude = Vector3.Magnitude(direction);

        if (directionMagnitude < range)
        {
            RaycastHit2D ray = Physics2D.RaycastAll(start, directionNormalised, directionMagnitude)[1];

            if (ray.collider.tag == "Wall")
            {
                aiPath.endReachedDistance = 0.2f;
                canFire = false;

            }
            else
            {
                aiPath.endReachedDistance = range;
                canFire = true;
            }
        }
        else
        {
            canFire = false;
        }




    }
}
