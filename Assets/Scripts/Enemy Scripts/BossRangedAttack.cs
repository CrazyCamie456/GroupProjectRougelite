using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossRangedAttack : MonoBehaviour
{
    private EnemyRangedAiPathHelper rangedAiPath;
    GameObject player;

    GameObject spearPrefab;

    public float setChargeAttackTime;
    private float chargeAttackTime = 0.0f;
    public float spearSpeed;
    public int damage;
    private CombatStats combatStats;

    void Start()
    {
        combatStats = GetComponent<CombatStats>();

        rangedAiPath = GetComponent<EnemyRangedAiPathHelper>();
        spearPrefab = Resources.Load<GameObject>("Prefabs/BossSpear");
        player = GameObject.Find("Player");

    }

    System.Guid myGUID = System.Guid.NewGuid();


    void Update()
    {
        if (rangedAiPath.canFire)
        {
            combatStats.ApplyCrowdControl(myGUID);
            if (chargeAttackTime < 0)
            {

                //fire enemy attack here
                var aimDirection = (player.transform.position - transform.position).normalized;


                GameObject enemySpear = Instantiate(spearPrefab, transform.position, ProjectileHelperFunctions.RotateToFace(aimDirection));
                enemySpear.GetComponent<DamageOnCollision>().Initialise("Player", damage);
                enemySpear.GetComponent<DestroySelfOnCollision>().Initialise(new List<string> { "Player", "Wall" });
                enemySpear.GetComponent<Rigidbody2D>().velocity = aimDirection * spearSpeed;





                chargeAttackTime = setChargeAttackTime;
                combatStats.RemoveCrowdControl(myGUID);
            }
            else
            {
                chargeAttackTime -= Time.deltaTime;
            }
        }
        else
        {
            combatStats.RemoveCrowdControl(myGUID);

        }
    }

}

