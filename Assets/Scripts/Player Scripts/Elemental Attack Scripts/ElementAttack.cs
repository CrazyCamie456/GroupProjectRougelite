using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElementAttack : MonoBehaviour
{
	public IElementalAttack elementalAttack;

	public float projectileSpeed = 10.0f;
	// In attacks per second.
	public float baseAttackSpeed = 1.0f;

	// Runtime variables
	float fireDelay = 0.0f;
	float currDelay = 0.0f;

	PlayerMovement playerMovement;
	PlayerController playerController;

	private void Awake()
	{
		playerController = new PlayerController();
	}

	private void OnEnable()
	{
		playerController.Enable();
	}
	private void OnDisable()
	{
		playerController.Disable();
	}

	void Start()
	{
		playerMovement = GetComponent<PlayerMovement>();
		elementalAttack = gameObject.AddComponent<ElementWaterAttack>();
		baseAttackSpeed = elementalAttack.GetBaseAttackSpeed();
	}

	void Update()
	{
		fireDelay = 1.0f / baseAttackSpeed;

		// If the attack button is down, and the player is aiming in any direction, and the players attack is off cooldown.
		if (playerController.Player.Attack.ReadValue<float>() > 0.5f &&
			(Mathf.Abs(playerMovement.directionAiming.x) > 0.1f || Mathf.Abs(playerMovement.directionAiming.y) > 0.1f)
			&& currDelay <= 0.0f)
		{
			currDelay = fireDelay;
			elementalAttack.Attack(playerMovement.directionAiming);
		}
		currDelay -= Time.deltaTime;

		//DEBUG SWAP ELEMENT
		if (playerController.Player.Debug_1.ReadValue<float>() > 0.5f)
		{
			Type temp = elementalAttack.GetType();
			Destroy(GetComponent(temp));
			elementalAttack = gameObject.AddComponent<ElementFireAttack>();
		}
		if (playerController.Player.Debug_2.ReadValue<float>() > 0.5f)
		{
			Type temp = elementalAttack.GetType();
			Destroy(GetComponent(temp));
			elementalAttack = gameObject.AddComponent<ElementWaterAttack>();
		}
	}
}
