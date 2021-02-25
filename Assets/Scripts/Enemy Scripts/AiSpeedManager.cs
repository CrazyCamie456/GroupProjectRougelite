using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AiSpeedManager : MonoBehaviour
{
    // Start is called before the first frame update
    private AIPath aiPath;
    private CombatStats combatStats;

    void Start(){
        aiPath = GetComponent<AIPath>();
        combatStats = GetComponent<CombatStats>();

        aiPath.maxSpeed = combatStats.baseMovementSpeed;
    }

    // Update is called once per frame
    void Update()
    {
        aiPath.maxSpeed = combatStats.baseMovementSpeed * Mathf.Max(1 + combatStats.bonusMovementSpeed,0);
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