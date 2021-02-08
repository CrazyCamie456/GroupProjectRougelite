using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BurnTargetOnHit : MonoBehaviour
{

	public List<string> targetLayers;
	public float duration;
	public float frequency;
	public int totalDamage;

	public void Initialise(string _targetLayer, int _totalDamage, float _duration, float _frequency)
	{
		targetLayers = new List<string>() { _targetLayer };
		totalDamage = _totalDamage;
		duration = _duration;
		frequency = _frequency;
	}

	public void Initialise(List<string> _targetLayers, int _totalDamage, float _duration, float _frequency)
	{
		targetLayers = _targetLayers;
		totalDamage = _totalDamage;
		duration = _duration;
		frequency = _frequency;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (targetLayers.Contains(collider.tag))
		{
			collider.gameObject.AddComponent<Burn>().Initialise(totalDamage, duration, frequency);
		}
	}
}

