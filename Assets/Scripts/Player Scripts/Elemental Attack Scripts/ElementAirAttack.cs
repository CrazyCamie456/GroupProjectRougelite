using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementAirAttack : MonoBehaviour, IElementalAttack
{
	// Compile time semi-static variables.
	GameObject airshotPrefab;
	float projectileSpeed = 20.0f; //20.0f;
	float attackSpeed = 7.5f;
	int damage = 3;

	void Start()
	{
		airshotPrefab = Resources.Load<GameObject>("Prefabs/Airshot");
	}

	public void Attack(Vector2 direction)
	{
		GameObject airshot = Instantiate(airshotPrefab, transform.position + (new Vector3(direction.x, direction.y, 0) * 0.4f), ProjectileHelperFunctions.RotateToFace(direction));
		airshot.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
		airshot.GetComponent<Rigidbody2D>().angularVelocity = 1440.0f;
		airshot.GetComponent<DamageOnCollision>().Initialise("Enemy", damage);
		airshot.GetComponent<DestroySelfOnCollision>().Initialise(new List<string> { "Enemy", "Wall" });
	}

	public float GetBaseAttackSpeed() { return attackSpeed; }

	public string Name() { return "Air"; }
}