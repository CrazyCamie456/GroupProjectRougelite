using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackSpeedBuff : MonoBehaviour
{
	PlayerCombatStats cs;
	
	public void Initialise(float value)
    {
		GetComponent<PlayerCombatStats>().bonusAttackSpeed = value;
	}
}
