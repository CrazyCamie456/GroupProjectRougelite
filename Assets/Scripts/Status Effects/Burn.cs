using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Burn : MonoBehaviour
{
	CombatStats cs;

	float duration;
	float frequency;
	int totalDamage;

	float damagePerTick;

	/*	There's a trick here for splitting a number into n ~equal integer chunks - if you take a number and remove 1/nth, then remove
		1/n-1th you'll have removed roughly 2/nths from the original number. Repeating this until you remove 1/1 from the number
		will leave you with the number divided as equally as possible between the ticks, while still using the entire number. */
	IEnumerator burnCoroutine()
	{
		int totalTicks = Mathf.FloorToInt(duration * frequency);
		int remainingTicks = totalTicks;
		int remainingDamage = totalDamage;

		float untilNextTick = 1.0f / frequency;

		while (remainingTicks > 0)
		{
			while (untilNextTick > 0.0f)
			{
				untilNextTick -= Time.deltaTime;
				yield return null;
			}
			untilNextTick += 1.0f / frequency;
			
			int damageThisTick = Mathf.FloorToInt(remainingDamage / remainingTicks);

			remainingDamage -= damageThisTick;
			remainingTicks--;

			cs.TakeDamage(damageThisTick);
		}
		Destroy(this);
	}

	// How how much damage should the burn do in total, how long should the burn last and how many times per second should it apply.
	public void Initialise(int _totalDamage, float _duration, float _frequency)
	{
		cs = GetComponent<CombatStats>();
		totalDamage = _totalDamage;
		duration = _duration;
		frequency = _frequency;
		StartCoroutine(burnCoroutine());
	}
}
