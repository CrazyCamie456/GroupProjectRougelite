using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AiSpeedManager : MonoBehaviour
{
    // Start is called before the first frame update
    private AIPath aiPath;
    private CombatStats combatStats;

    void Awake(){
        aiPath = GetComponent<AIPath>();
        combatStats = GetComponent<CombatStats>();
    }

    // Update is called once per frame
    void Update()
    {
        aiPath.maxSpeed = combatStats.movementSpeed;
        if (combatStats.isCrowdControlled)
        {
            aiPath.canMove = false;
        }
        else
        {
            aiPath.canMove = true;

        }
    }
}