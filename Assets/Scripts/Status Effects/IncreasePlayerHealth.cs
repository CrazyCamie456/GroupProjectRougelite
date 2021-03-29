using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePlayerHealth : MonoBehaviour
{
	public float amount;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Player")
		{
			collision.gameObject.GetComponent<CombatStats>().maxHealth += 2;
			Destroy(gameObject);
		}
	}
}
