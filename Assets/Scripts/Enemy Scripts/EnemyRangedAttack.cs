using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class EnemyRangedAttack : MonoBehaviour
{
    private AIPath aiPath;
    private EnemyRangedAiPathHelper rangedAiPath;
    GameObject player;

    GameObject spearPrefab;

    public float setChargeAttackTime;
    private float chargeAttackTime = 0.0f;
    public float spearSpeed;
    // Start is called before the first frame update
    void Start()
    {
        aiPath = GetComponent<AIPath>();
        rangedAiPath = GetComponent<EnemyRangedAiPathHelper>();
        spearPrefab = Resources.Load<GameObject>("Prefabs/Spear");
        player = GameObject.Find("Player");


    }

    /* to attack:
     * 
     * stop moving - aiPath.canMove = false;
     * change attack
     * fire attack
     * start moving - aiPath.canMove = true;
     * 
     */





    // Update is called once per frame
    void Update()
    {
        if (rangedAiPath.canFire)
        {
            aiPath.canMove = false;
            if (chargeAttackTime < 0)
            {

                //fire enemy attack here
                var aimDirection = (player.transform.position - transform.position).normalized;


                GameObject enemySpear = Instantiate(spearPrefab, transform.position, ProjectileHelperFunctions.RotateToFace(aimDirection));
                enemySpear.GetComponent<DamageOnCollision>().Initialise("Player", 2);
                enemySpear.GetComponent<DestroySelfOnCollision>().Initialise(new List<string> { "Player", "Wall" });
                enemySpear.GetComponent<Rigidbody2D>().velocity = aimDirection * spearSpeed;





                chargeAttackTime = setChargeAttackTime;
                aiPath.canMove = true;
            }
            else
            {
                chargeAttackTime -= Time.deltaTime;
            }
        }
    }

}
