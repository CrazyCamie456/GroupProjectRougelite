using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileHelperFunctions
{
    public static Quaternion RotateToFace(Vector2 direction)
	{
		float projectileZRotation;

		// Get the rotation angle of the projectile from up, which is used to ensure the projectile is created with the correct orientation.
		projectileZRotation = (180.0f / Mathf.PI) * Mathf.Acos((Vector2.Dot(direction, Vector2.up) / direction.magnitude));

		// Accounting for Acos only returning positive values, regardless of which side of the player the fireball comes from.
		if (direction.x > 0.0f)
			projectileZRotation *= -1.0f;

		// And make it into a Quaternion, which can be passed to Instantiate(gameObject, transform, Quaternion);
		return Quaternion.Euler(0.0f, 0.0f, projectileZRotation);
	}
}
