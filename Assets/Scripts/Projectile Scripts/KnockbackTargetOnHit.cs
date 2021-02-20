using System.Collections.Generic;
using UnityEngine;

public class KnockbackTargetOnHit : MonoBehaviour
{
	public List<string> targetLayers;
	public float duration;
	public float distance;
	public Vector2 direction;

	public void Initialise(string _targetLayer, float _duration, float _distance)
	{
		targetLayers = new List<string>() { _targetLayer };
		duration = _duration;
		distance = _distance;
		direction = Vector2.zero;
	}

	public void Initialise(List<string> _targetLayers, float _duration, float _distance)
	{
		targetLayers = _targetLayers;
		duration = _duration;
		distance = _distance;
		direction = Vector2.zero;
	}

	public void Initialise(string _targetLayer, float _duration, float _distance, Vector2 _direction)
	{
		targetLayers = new List<string>() { _targetLayer };
		duration = _duration;
		distance = _distance;
		direction = _direction;
	}

	public void Initialise(List<string> _targetLayers, float _duration, float _distance, Vector2 _direction)
	{
		targetLayers = _targetLayers;
		duration = _duration;
		distance = _distance;
		direction = _direction;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (targetLayers.Contains(collider.tag))
		{
			if (direction == Vector2.zero)
			{
				Vector2 knockbackDirection = GetComponent<Rigidbody2D>().velocity;
				knockbackDirection.Normalize();
				collider.gameObject.AddComponent<Knockback>().Initialise(duration, distance, knockbackDirection);
				return;
			}
			collider.gameObject.AddComponent<Knockback>().Initialise(duration, distance, direction);

		}
	}
}
