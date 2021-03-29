using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpeedBuff : MonoBehaviour
{
	PlayerCombatStats cs;

	public void Initialise(float value)
	{
		GetComponent<PlayerCombatStats>().bonusMovementSpeed = value;
	}
}
