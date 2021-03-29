﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementEarthAttack : MonoBehaviour, IElementalAttack
{
	// Compile time semi-static variables.
	GameObject airshotPrefab;
	float projectileSpeed = 9.0f;
	float attackSpeed = 1.5f;
	int damage = 15;

	void Start()
	{
		airshotPrefab = Resources.Load<GameObject>("Prefabs/Earthshot");
	}

	public void Attack(Vector2 direction)
	{
		GameObject earthshot = Instantiate(airshotPrefab, transform.position + (new Vector3(direction.x, direction.y, 0) * 0.4f), Quaternion.identity);
		earthshot.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
		earthshot.GetComponent<DamageOnCollision>().Initialise("Enemy", damage);
		earthshot.GetComponent<DestroySelfOnCollision>().Initialise(new List<string> { "Enemy", "Wall" });
		earthshot.GetComponent<KnockbackTargetOnHit>().Initialise("Enemy", 0.1f, 0.8f);
	}

	public float GetBaseAttackSpeed() { return attackSpeed; }

	public string Name() { return "Earth"; }
}