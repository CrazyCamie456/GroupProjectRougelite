using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiseUserSettings : MonoBehaviour
{
    void Awake()
	{
		int resolution = PlayerPrefs.GetInt("Resolution");
		bool fullscreen = PlayerPrefs.GetInt("Fullscreen") == 1;
		Screen.SetResolution(
			ResolutionSettingsUIController.resolutionOptions[resolution].x,
			ResolutionSettingsUIController.resolutionOptions[resolution].y,
			fullscreen
		);
		ResolutionSettingsUIController.index = resolution;
		ResolutionSettingsUIController.isFullscreen = fullscreen;
	}
}
