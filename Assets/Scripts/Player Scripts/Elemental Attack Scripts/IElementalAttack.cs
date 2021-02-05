using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IElementalAttack
{
	string Name();
	void Attack(Vector2 direction);
	float GetBaseAttackSpeed();
}
