using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
	public int health;
	public int maxHealth;

	public void Initialise(int _maxHealth)
	{
		maxHealth = _maxHealth;
		health = maxHealth;
	}

	public void Initialise(int _health, int _maxHealth)
	{
		health = _health;
		maxHealth = _maxHealth;
	}

	public void TakeDamage(int damage)
	{
		health -= damage;
		if (health <= 0) Destroy(gameObject);
	}

	void Start()
    {
		health = maxHealth;
    }

	void Update()
	{
		
	}
}