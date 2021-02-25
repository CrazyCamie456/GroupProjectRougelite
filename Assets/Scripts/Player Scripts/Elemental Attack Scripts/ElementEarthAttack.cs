using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementEarthAttack : MonoBehaviour, IElementalAttack
{
	// Compile time semi-static variables.
	GameObject airshotPrefab;
	float projectileSpeed = 4.0f;
	float attackSpeed = 1.0f;

	void Start()
	{
		airshotPrefab = Resources.Load<GameObject>("Prefabs/Earthshot");
	}

	public void Attack(Vector2 direction)
	{
		GameObject earthshot = Instantiate(airshotPrefab, transform.position, Quaternion.identity);
		earthshot.GetComponent<Rigidbody2D>().velocity = direction * projectileSpeed;
		earthshot.GetComponent<DamageOnCollision>().Initialise("Enemy", 6);
		earthshot.GetComponent<DestroySelfOnCollision>().Initialise(new List<string> { "Enemy", "Wall" });
		earthshot.GetComponent<KnockbackTargetOnHit>().Initialise("Enemy", 0.2f, 0.6f);
	}

	public float GetBaseAttackSpeed() { return attackSpeed; }

	public string Name() { return "Earth"; }
}