using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementFireAttack : MonoBehaviour, IElementalAttack
{
	// Compile time semi-static variables.
	GameObject fireballPrefab;
	float projectileSpeed = 10.0f;
	float attackSpeed = 2.0f;

	void Start()
	{
		fireballPrefab = Resources.Load<GameObject>("Prefabs/Fireball");
	}

	public void Attack(Vector2 direction)
	{
		GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);
		fireball.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
		fireball.GetComponent<DamageOnCollision>().Initialise("Enemy", 2);
		fireball.GetComponent<DestroySelfOnCollision>().Initialise("Enemy");
	}

	public float GetBaseAttackSpeed() { return attackSpeed; }

	public string Name() { return "Fire"; }
}