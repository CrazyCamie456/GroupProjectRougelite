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

		if (PlayerPrefs.HasKey("MasterVolume"))
			AkSoundEngine.SetRTPCValue("MasterVolume", PlayerPrefs.GetInt("MasterVolume"));
		else
			AkSoundEngine.SetRTPCValue("MasterVolume", 100);
		if (PlayerPrefs.HasKey("SFXVolume"))
			AkSoundEngine.SetRTPCValue("SFXVolume", PlayerPrefs.GetInt("SFXVolume"));
		else
			AkSoundEngine.SetRTPCValue("SFXVolume", 100);
		if (PlayerPrefs.HasKey("MusicVolume"))
			AkSoundEngine.SetRTPCValue("MusicVolume", PlayerPrefs.GetInt("MusicVolume"));
		else
			AkSoundEngine.SetRTPCValue("MusicVolume", 100);
	}
}
