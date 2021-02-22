using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class ChargedAttacks : MonoBehaviour
{

    public float attackRange;
    GameObject player;
    private Vector3 playerPosition;
    public float setChargeAttackTime;
    private float chargeAttackTime = 0.0f;
    public float attackSpeed;
    private CombatStats combatStats;
    public float setAttackDelay;
    private float attackDelay = 0.0f;
    bool isCharging = false;
    void Start()
    {
        player = GameObject.Find("Player");
        combatStats = GetComponent<CombatStats>();

        chargeAttackTime = setChargeAttackTime;
    }
    System.Guid myGUID = System.Guid.NewGuid();
    /*
     * stop moving
     * charge attack
     * dash foward
     * move again
     */

    void Update()
    {
        playerPosition = player.transform.position;
        if (attackDelay < 0.0f)
        {
            if (Vector3.Magnitude(playerPosition - transform.position) < attackRange)
            {
                if(!isCharging)
                {
                    isCharging = true;
                    StartCoroutine(ChargeCoroutine());

                }

            }
        }
        else
        {
            attackDelay -= Time.deltaTime;
        }


    }

    IEnumerator ChargeCoroutine()
    {
        combatStats.ApplyCrowdControl(myGUID);

        while (chargeAttackTime > 0.0f)
        {
            chargeAttackTime -= Time.deltaTime;
            yield return null;

        }
        isCharging = false;
        //fire enemy attack here
        GetComponent<Rigidbody2D>().velocity = (playerPosition - transform.position) * attackSpeed;

        chargeAttackTime = setChargeAttackTime;
        Debug.Log("attacked");
        attackDelay = setAttackDelay;
        combatStats.RemoveCrowdControl(myGUID);
    }
}
