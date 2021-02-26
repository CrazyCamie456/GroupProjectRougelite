using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExitManager : MonoBehaviour
{
	private PlayerController playerController;
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
	private void Update()
	{
		if (playerController.Player.Escape.ReadValue<float>() > 0.5f)
			Application.Quit();
	}
}
