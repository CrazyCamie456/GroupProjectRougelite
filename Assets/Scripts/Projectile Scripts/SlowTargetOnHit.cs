using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SlowTargetOnHit : MonoBehaviour
{

	public List<string> targetLayers;
	public float slowPotency;
	public float slowDuration;

	public void Initialise(string _targetLayer, float _slowPotency, float _slowDuration)
	{
		targetLayers = new List<string>() { _targetLayer };
		slowPotency = _slowPotency;
		slowDuration = _slowDuration;
	}

	public void Initialise(List<string> _targetLayers, float _slowPotency, float _slowDuration)
	{
		targetLayers = _targetLayers;
		slowPotency = _slowPotency;
		slowDuration = _slowDuration;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (targetLayers.Contains(collider.tag))
		{
			collider.gameObject.AddComponent<Slow>().Initialise(slowPotency, slowDuration);
		}
	}
}
