using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementWaterAttack : MonoBehaviour, IElementalAttack
{
	// Compile time semi-static variables.
	GameObject waterShotPrefab;
	float projectileSpeed = 8.0f;
	float attackSpeed = 1.25f;

	void Start()
	{
		waterShotPrefab = Resources.Load<GameObject>("Prefabs/Watershot");
	}

	public void Attack(Vector2 direction)
	{
		GameObject waterShot = Instantiate(waterShotPrefab, transform.position, Quaternion.identity);
		waterShot.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
		waterShot.GetComponent<DamageOnCollision>().Initialise("Enemy", 3);
		waterShot.GetComponent<DestroySelfOnCollision>().Initialise("Enemy");
	}

	public float GetBaseAttackSpeed() { return attackSpeed; }

	public string Name() { return "Water"; }
}
