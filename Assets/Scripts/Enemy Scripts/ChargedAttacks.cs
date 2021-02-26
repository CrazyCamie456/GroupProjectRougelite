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
    public float SetAttackDuration;
    private float attackDuration;
    private float attackCoolDown;
    public float setAttackCoolDown;
    private CombatStats combatStats;
    public float setAttackDelay;
    private float attackDelay = 0.0f;
    bool isCharging = false;
    void Start()
    {
        player = GameObject.Find("Player");
        combatStats = GetComponent<CombatStats>();
        attackDuration = SetAttackDuration;
        chargeAttackTime = setChargeAttackTime;
    }
    System.Guid myGUID = System.Guid.NewGuid();

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
        else if(!isCharging)
        {
            attackDelay -= Time.deltaTime;
        }


    }

    IEnumerator ChargeCoroutine()
    {
        combatStats.ApplyCrowdControl(myGUID);

        while (chargeAttackTime > 0.0f)
        {
            Debug.Log("charge");

            yield return null;
            chargeAttackTime -= Time.deltaTime;
        }
       
        //fire enemy attack here
        Vector3 attackDistance = playerPosition - transform.position;
        Vector3 attackVelocity = (attackDistance/ attackDuration).normalized;
        float tempSpeedModifier = (1.0f + Mathf.Min(combatStats.bonusMovementSpeed, 0.0f));
        float speedmodifier = Mathf.Max(0.0f, tempSpeedModifier);
        GetComponent<Rigidbody2D>().velocity = attackVelocity* speedmodifier*attackSpeed;
        while (attackDuration>0.0f)
        {
            Debug.Log("attack");
            yield return null;

            attackDuration -= Time.deltaTime;
        }

        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
        
        while (attackCoolDown > 0.0f)
        {
            Debug.Log("cooldown");
            yield return null;
            attackCoolDown -= Time.deltaTime;
        }

        isCharging = false;
        attackDuration = SetAttackDuration;
        attackDelay = setAttackDelay;
        chargeAttackTime = setChargeAttackTime;
        attackCoolDown = setAttackCoolDown;
        combatStats.RemoveCrowdControl(myGUID);
    }
}