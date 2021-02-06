using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slow : MonoBehaviour
{
	CombatStats cs;

	float duration;
	float potency;

	IEnumerator slowCoroutine()
	{
		cs.bonusMovementSpeed -= potency;
		float remainingDuration = duration;
		while (remainingDuration > 0)
		{
			yield return null;
			remainingDuration -= Time.deltaTime;
		}
		cs.bonusMovementSpeed += potency;
	}

	public void Initialise(float _potency, float _duration)
	{
		cs = GetComponent<CombatStats>();
		potency = _potency;
		duration = _duration;
		StartCoroutine(slowCoroutine());
	}
}
