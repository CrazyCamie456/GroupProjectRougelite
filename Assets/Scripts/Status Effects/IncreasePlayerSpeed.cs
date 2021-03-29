using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IncreasePlayerSpeed : MonoBehaviour
{
	public float amount;

	private void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.gameObject.name == "Player")
		{
			collision.gameObject.AddComponent<SpeedBuff>().Initialise(0.5f);
			Destroy(gameObject);
		}
	}
}