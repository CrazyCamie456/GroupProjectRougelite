using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePlayer : MonoBehaviour
{
    private CombatStats combatStats;
    public float baseDamage;
    private string collisionTag = "Player";
    void Start()
    {
        combatStats = GetComponent<CombatStats>();
    }

    public void Initialise(float _damage)
    {
        baseDamage = _damage;
    }

    void OnCollisionStay2D(Collision2D col)
    {
        //If colliding with the chosen tag
        if (col.gameObject.tag == collisionTag)
        {
            //Calculate the damage based on a base damage stat and the bonus attack damage they have
            int damageTaken = Mathf.FloorToInt(baseDamage + (baseDamage * combatStats.bonusAttackDamage));
            if (damageTaken > 0)
                col.gameObject.GetComponent<CombatStats>().TakeDamage(damageTaken);
        }
    }
}