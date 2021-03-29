using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseHandler : MonoBehaviour
{
	private PlayerController playerController;
	Transform[] childTransforms;

	private void Awake()
	{
		playerController = new PlayerController();
	}

	private void Start()
	{
		childTransforms = GetComponentsInChildren<Transform>();
		SetEnabledOnAllChildren(false);
	}

	private void OnEnable()
	{
		playerController.Enable();
	}
	private void OnDisable()
	{
		playerController.Disable();
	}

	bool escLastFrame = false;
	bool isPauseMenuUp = false;

	void SetEnabledOnAllChildren(bool newValue)
	{
		foreach (Transform t in childTransforms)
		{
			if (t != transform)
			{
				t.gameObject.SetActive(newValue);
			}
		}
	}

	void Update()
	{
		if (playerController.Player.Escape.ReadValue<float>() > 0.5f)
		{
			if (escLastFrame == false)
			{
				if (isPauseMenuUp)
				{
					Time.timeScale = 1.0f;
					SetEnabledOnAllChildren(false);
					isPauseMenuUp = false;
				} else
				{
					Time.timeScale = 0.0f;
					SetEnabledOnAllChildren(true);
					isPauseMenuUp = true;
				}
			}
			escLastFrame = true;
		} else
		{
			escLastFrame = false;
		}
    }
}
