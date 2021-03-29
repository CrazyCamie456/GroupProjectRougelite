using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementWaterAttack : MonoBehaviour, IElementalAttack
{
	// Compile time semi-static variables.
	GameObject waterShotPrefab;
	float projectileSpeed = 12.0f;
	float attackSpeed = 1.25f;
	int damage = 6;

	void Start()
	{
		waterShotPrefab = Resources.Load<GameObject>("Prefabs/Watershot");
	}

	public void Attack(Vector2 direction)
	{
		GameObject waterShot = Instantiate(waterShotPrefab, transform.position + (new Vector3(direction.x, direction.y, 0) * 0.4f), ProjectileHelperFunctions.RotateToFace(direction));
		waterShot.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
		waterShot.GetComponent<DamageOnCollision>().Initialise("Enemy", damage);
		waterShot.GetComponent<DestroySelfOnCollision>().Initialise(new List<string> { "Enemy", "Wall" });
		waterShot.GetComponent<SlowTargetOnHit>().Initialise("Enemy", 0.3f, 2.0f);
	}

	public float GetBaseAttackSpeed() { return attackSpeed; }

	public string Name() { return "Water"; }
}
