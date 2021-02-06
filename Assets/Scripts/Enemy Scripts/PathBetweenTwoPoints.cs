using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathBetweenTwoPoints : MonoBehaviour
{
	public Vector2 pointA;
	public Vector2 pointB;
	bool movingTowardsB;
	Rigidbody2D rb;
	CombatStats cs;

	void Start()
	{
		movingTowardsB = true;
		cs = GetComponent<CombatStats>();
		rb = GetComponent<Rigidbody2D>();
	}

	void Update()
	{
		if (movingTowardsB)
		{
			if (Vector2.Distance(rb.position, pointB) < 0.2f)
			{
				movingTowardsB = !movingTowardsB;
			}
			rb.velocity = (pointB - rb.position).normalized * cs.movementSpeed;
		}
		else
		{
			if (Vector2.Distance(rb.position, pointA) < 0.2f)
			{
				movingTowardsB = !movingTowardsB;
			}
			rb.velocity = (pointA - rb.position).normalized * cs.movementSpeed;
		}
	}
}
