using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroySelfOnCollision : MonoBehaviour
{
	
	public List<string> targetLayers;

	public void Initialise(string _targetLayer)
	{
		targetLayers = new List<string>() { _targetLayer };
	}

	public void Initialise(List<string> _targetLayers)
	{
		targetLayers = _targetLayers;
	}

	void OnTriggerEnter2D(Collider2D collider)
	{
		if (targetLayers.Contains(collider.tag))
		{
			Destroy(gameObject);
		}
	}
}
