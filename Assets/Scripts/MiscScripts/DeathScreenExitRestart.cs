using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class DeathScreenExitRestart : MonoBehaviour
{
	private PlayerController playerController;
	PlayerInput pi;
	Text text;

	private void Awake()
	{
		playerController = new PlayerController();
		pi = GetComponent<PlayerInput>();
		text = GameObject.Find("Text2").GetComponent<Text>();
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
		string restartButton = "Space";
		string quitButton = "Esc";

		switch (pi.currentControlScheme)
		{
			case "Mouse":
			case "Keyboard":
				restartButton = "Space";
				quitButton = "Escape";
				break;
			case "Xbox":
				restartButton = "B";
				quitButton = "Back";
				break;
			case "DualShock 4":
				restartButton = "Circle";
				quitButton = "Touchpad";
				break;
		}
		text.text = "Press [" + restartButton + "] to restart or [" + quitButton + "] to quit.";
		if (playerController.Player.Escape.ReadValue<float>() > 0.5f)
			Application.Quit();
		if (playerController.Player.Dash.ReadValue<float>() > 0.5f)
			SceneManager.LoadScene(0);
	}

	public void onControlsChanged()
	{

	}
}
