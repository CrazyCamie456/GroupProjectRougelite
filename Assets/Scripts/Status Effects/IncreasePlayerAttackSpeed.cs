using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePlayerAttackSpeed : MonoBehaviour
{
	public float amount;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Player")
		{
			collision.gameObject.AddComponent<AttackSpeedBuff>().Initialise(1.0f);
			Destroy(gameObject);
		}
	}
}
