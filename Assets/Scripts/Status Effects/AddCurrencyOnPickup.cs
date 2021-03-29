using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddCurrencyOnPickup : MonoBehaviour
{
	public int value;
	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Player")
		{
			collision.gameObject.GetComponent<PlayerCombatStats>().currency += value;
			Destroy(gameObject);
		}
	}
}
