using UnityEngine;

public static class VolumeSettings
{
	public static int sfxVolume = 100;
	public static int musicVolume = 100;

	[SerializeField] private static int pMainVolume;
	[SerializeField] private static int pSfxVolume;
	[SerializeField] private static int pMusicVolume;

	public static void ChangeMainVolumeSetting(float newValue)
	{
		pMainVolume = (int)newValue;
		sfxVolume = pMainVolume * pSfxVolume;
		musicVolume = pMainVolume * pMusicVolume;
		PlayerPrefs.SetInt("MainVolume", (int)newValue);
	}
	public static void ChangeSFXVolumeSetting(float newValue)
	{
		pSfxVolume = (int)newValue;
		sfxVolume = pMainVolume * pSfxVolume;
		PlayerPrefs.SetInt("SFXVolume", (int)newValue);
	}
	public static void ChangeMusicVolumeSetting(float newValue)
	{
		pMusicVolume = (int)newValue;
		musicVolume = pMainVolume * pMusicVolume;
		PlayerPrefs.SetInt("MusicVolume", (int)newValue);
	}
}
