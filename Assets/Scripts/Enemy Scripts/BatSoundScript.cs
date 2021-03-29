using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BatSoundScript : MonoBehaviour
{
	Rigidbody2D rb;
	CombatStats cs;
	bool eventPlaying;

	void Start()
	{
		rb = GetComponent<Rigidbody2D>();
		cs = GetComponent<CombatStats>();
		eventPlaying = false;
	}

	void Update()
	{
		// If not crowd controlled and has a velocity
		if (!(cs.isCrowdControlled) && (rb.velocity.x > 0.1f || rb.velocity.x < -0.1f || rb.velocity.y > 0.1f || rb.velocity.y < -0.1f))
		{
			if (!eventPlaying)
			{
				eventPlaying = true;
				AkSoundEngine.PostEvent("Play_Bat_Wings", gameObject);
			}
		}
		else
		{
			eventPlaying = false;
			AkSoundEngine.PostEvent("Stop_Bat_Wings", gameObject);
		}
	}

	void OnDestroy()
	{
		eventPlaying = false;
		AkSoundEngine.PostEvent("Stop_Bat_Wings", gameObject);
	}
}
