using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageOnCollision : MonoBehaviour
{
	
	public List<string> targetLayers;
	public int damage;

	public void Initialise(string _targetLayer, int _damage)
	{
		targetLayers = new List<string>() { _targetLayer };
		damage = _damage;
	}

	public void Initialise(List<string> _targetLayers, int _damage)
	{
		targetLayers = _targetLayers;
		damage = _damage;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (targetLayers.Contains(collider.tag))
		{
			collider.GetComponent<EnemyHealth>().TakeDamage(damage);
		}
	}
}
