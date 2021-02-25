using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// ELEMENTS 0-5 MUST ALWAYS REMAIN NONE, FIRE, WATER, EARTH, AIR - they are being used for randomly generating base elements using unchecked typecasting.
// Elements 6+ (if they exist) can change order as needed - any comparisons should use this enum.
public enum Element
{
	None = 0,
	Fire,
	Water,
	Earth,
	Air
};

public class ElementAttack : MonoBehaviour
{
	const int DEBUG_MAX_ELEMENTS = 1;

	public IElementalAttack elementalAttack;
	[HideInInspector]
	public List<Element> elements;

	// In attacks per second.
	public float baseAttackSpeed { get; private set; }

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
		elements = new List<Element>();
		for (int i = 0; i < DEBUG_MAX_ELEMENTS; i++)
		{
			elements.Add(Element.None);
		}

		elementalAttack = gameObject.AddComponent<ElementNoneAttack>();
		baseAttackSpeed = elementalAttack.GetBaseAttackSpeed();
	}

	// Returns whether the element was successfully added.
	public bool AddElement(Element e)
	{
		bool succeeded = false;
		if (elements[0] == Element.None)
		{
			elements[0] = e;
			succeeded = true;
		}
		if (DEBUG_MAX_ELEMENTS > 1 && elements[1] == Element.None)
		{
			elements[1] = e;
			succeeded = true;
		}
		if (DEBUG_MAX_ELEMENTS > 2 && elements[2] == Element.None)
		{
			elements[2] = e;
			succeeded = true;
		}
		if (!succeeded)
			Debug.Log("Player has max elements, adding more failed.");
		UpdateElement();
		return succeeded;
	}

	void UpdateElement()
	{
		if (elements.Count == 0) return;
		Type temp = elementalAttack.GetType();
		Destroy(GetComponent(temp));

		switch (elements[0])
		{
			case Element.None:
				elementalAttack = gameObject.AddComponent<ElementNoneAttack>();
				break;
			case Element.Fire:
				elementalAttack = gameObject.AddComponent<ElementFireAttack>();
				break;
			case Element.Water:
				elementalAttack = gameObject.AddComponent<ElementWaterAttack>();
				break;
			case Element.Earth:
				elementalAttack = gameObject.AddComponent<ElementEarthAttack>();
				break;
			case Element.Air:
				elementalAttack = gameObject.AddComponent<ElementAirAttack>();
				break;
			default:
				elementalAttack = gameObject.AddComponent<ElementNoneAttack>();
				break;
		}
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
			elements[0] = Element.Fire;
			UpdateElement();
		}
		if (playerController.Player.Debug_2.ReadValue<float>() > 0.5f)
		{
			elements[0] = Element.Water;
			UpdateElement();
		}
		if (playerController.Player.Debug_3.ReadValue<float>() > 0.5f)
		{
			elements[0] = Element.Earth;
			UpdateElement();
		}
		if (playerController.Player.Debug_4.ReadValue<float>() > 0.5f)
		{
			elements[0] = Element.Air;
			UpdateElement();
		}
		if (playerController.Player.Debug_5.ReadValue<float>() > 0.5f)
		{
			elements[0] = Element.None;
			UpdateElement();
		}

	}
}
