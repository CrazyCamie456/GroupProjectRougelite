using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All "bonus" values should be as a multiplier, eg 0.2f is a 20% increase to the relative stats, a negative value is a debuff.
public class CombatStats : MonoBehaviour
{
	public int maxHealth;
	// Ensure the entity can never be healed to above their maximum health.
	public int currentHealth
	{
		get
		{
			return pCurrentHealth;
		}
		set => pCurrentHealth = Mathf.Min(value, maxHealth);
	}
	private int pCurrentHealth;

	public float baseMovementSpeed;
	public float bonusMovementSpeed;
	public float movementSpeed { get { return Mathf.Max(baseMovementSpeed * (1 + bonusMovementSpeed), 0.0f); } }

	public void TakeDamage(int damage)
	{
		currentHealth -= damage;
		if (currentHealth < 0)
			Destroy(gameObject);
	}

	// Base attack damage and attack speed should be managed by the specific attack,
	// this means an enemy with multiple attack options could have different base damage or speed.
	public float bonusAttackDamage;
	public float bonusAttackSpeed;

	void Start()
	{
		// Health can start as a non-full value, but if it is unset, default to max health.
		if (currentHealth == 0)
			currentHealth = maxHealth;
	}
}