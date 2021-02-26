using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCombatStats : CombatStats
{
    public float setIFrames;
    private float iFrames=-1.0f;

    public override void TakeDamage(int damage)
    {
        if (iFrames < 0)
        {
			base.TakeDamage(damage);
			iFrames = setIFrames;
            StartCoroutine(ReduceIFrames());
        }
    }

    IEnumerator ReduceIFrames()
    {
        while(iFrames > 0)
        {
            iFrames -= Time.deltaTime;
            yield return null;
        }
    }

}
