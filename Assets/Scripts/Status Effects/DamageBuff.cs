using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageBuff : MonoBehaviour
{
	PlayerCombatStats cs;

	public void Initialise(float value)
	{
		GetComponent<PlayerCombatStats>().bonusAttackDamage = value;
	}
}
