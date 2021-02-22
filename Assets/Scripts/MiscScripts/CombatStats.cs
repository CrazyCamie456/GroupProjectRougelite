using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// All "bonus" values should be as a multiplier, eg 0.2f is a 20% increase to the relative stats, a negative value is a debuff.
public class CombatStats : MonoBehaviour
{

	public int maxHealth;
	// Ensure the entity can never be healed to above their maximum health.
	public int currentHealth { get; protected set; }

	public float baseMovementSpeed;
	public float bonusMovementSpeed;
	public float movementSpeed { get { return Mathf.Max(baseMovementSpeed * (1 + bonusMovementSpeed), 0.0f); } }

	// Base attack damage and attack speed should be managed by the specific attack,
	// this means an enemy with multiple attack options could have different base damage or speed.
	public float bonusAttackDamage;
	public float bonusAttackSpeed;

	// Stores and makes available whether the object is crowd controlled.
	private List<Guid> crowdControlList;
	public bool isCrowdControlled
	{
		get
		{
			return crowdControlList.Count > 0;
		}
	}
	public void ApplyCrowdControl(Guid crowdControlID)
	{
		crowdControlList.Add(crowdControlID);
	}
	public void RemoveCrowdControl(Guid crowdControlID)
	{
		crowdControlList.Remove(crowdControlID);
	}
	// Just a helper function, centralizing the creation of Crowd Controls. Feel free to just use the NewGuid() function in your own code.
	public Guid NewCrowdControlID()
	{
		return Guid.NewGuid();
	}


	public virtual void TakeDamage(int damage)
	{
		currentHealth -= damage;
		if (currentHealth <= 0)
			Destroy(gameObject);
	}

	void Start()
	{
		// Health can start as a non-full value, but if it is unset, default to max health.
		if (currentHealth == 0)
			currentHealth = maxHealth;
		crowdControlList = new List<Guid>();
	}
}