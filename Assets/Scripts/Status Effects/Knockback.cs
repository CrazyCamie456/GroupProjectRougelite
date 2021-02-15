using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Knockback : MonoBehaviour
{
	CombatStats cs;
	Rigidbody2D rb;

	float duration;
	Vector2 direction;
	float totalDistance;

	IEnumerator KnockbackCoroutine()
	{
		Guid CCID = cs.NewCrowdControlID();
		cs.ApplyCrowdControl(CCID);
		float remainingDuration = duration;
		float distancePerSecond = totalDistance / duration;
		while (remainingDuration > 0.0f)
		{
			rb.velocity = direction * distancePerSecond;
			yield return null;
			remainingDuration -= Time.deltaTime;
		}
		rb.velocity = Vector2.zero;
		cs.RemoveCrowdControl(CCID);
		Destroy(this);
	}

	public void Initialise(float _duration, float _distance, Vector2 _direction)
	{
		cs = GetComponent<CombatStats>();
		rb = GetComponent<Rigidbody2D>();
		duration = _duration;
		totalDistance = _distance;
		direction = _direction;
		StartCoroutine(KnockbackCoroutine());
	}
}
