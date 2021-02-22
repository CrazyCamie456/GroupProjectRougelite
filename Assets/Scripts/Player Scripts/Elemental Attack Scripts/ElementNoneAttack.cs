using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementNoneAttack : MonoBehaviour, IElementalAttack
{
	// Makes the maximum possible delay a player can have when picking up a new element 1ms.
	// IF THIS ATTACK STARTS DOING SOMETHING LOWER THIS.
	float attackSpeed = 1000.0f;

	public void Attack(Vector2 direction)
	{
		/*
		If it is decided that the player has an attack that has no element, this script can house it,
		additionally, it should speed up/simplify some code in ElementalAttack

		If not, just leave this section blank, no harm really.
		*/
	}

	public float GetBaseAttackSpeed() { return attackSpeed; }

	public string Name() { return "None"; }
}
