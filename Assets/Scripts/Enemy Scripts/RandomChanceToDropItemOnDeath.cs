using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomChanceToDropItemOnDeath : MonoBehaviour
{
	public GameObject item;
	[Range(0.0f, 1.0f)]
	public float dropProbability;

	static bool isQuitting = false;

	// This solves the bug with gameObjects (corpses) being created when the game is quit in the editor.
	[RuntimeInitializeOnLoadMethod] static void RunOnStart() { Application.quitting += () => isQuitting = true; }

	void Start()
	{
		dropProbability = Mathf.Max(dropProbability, 0.0f);
	}

	void OnDestroy()
	{
		if (isQuitting) return;

		// The 0.9999 is to be certain floating point rounding doesn't cause a "100%" drop to fail.
		float r = Random.Range(0.0f, 0.9999f);
		Debug.Log(r + " ?< " + dropProbability);
		if (r <= dropProbability)
		{
			Instantiate(item, transform.position, Quaternion.identity);
		}
	}
}
