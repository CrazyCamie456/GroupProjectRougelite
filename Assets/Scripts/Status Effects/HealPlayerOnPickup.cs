using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealPlayerOnPickup : MonoBehaviour
{
	public int amount;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Player")
		{
			collision.gameObject.GetComponent<PlayerCombatStats>().Heal(amount);
			Destroy(gameObject);
		}
	}
}

