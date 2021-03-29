using UnityEngine;

public static class VolumeSettings
{
	public static void ChangeMainVolumeSetting(float newValue)
	{
		AkSoundEngine.SetRTPCValue("MasterVolume", newValue);
		PlayerPrefs.SetInt("MasterVolume", (int)newValue);
	}
	public static void ChangeSFXVolumeSetting(float newValue)
	{
		AkSoundEngine.SetRTPCValue("SFXVolume", newValue);
		PlayerPrefs.SetInt("SFXVolume", (int)newValue);
	}
	public static void ChangeMusicVolumeSetting(float newValue)
	{
		AkSoundEngine.SetRTPCValue("MusicVolume", newValue);
		PlayerPrefs.SetInt("MusicVolume", (int)newValue);
	}
}
