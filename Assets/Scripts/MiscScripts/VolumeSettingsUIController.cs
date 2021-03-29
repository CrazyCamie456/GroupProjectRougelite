using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeSettingsUIController : MonoBehaviour
{
	public void ChangeMainVolumeSetting(float newValue)
	{
		VolumeSettings.ChangeMainVolumeSetting(newValue);
	}
	public void ChangeSFXVolumeSetting(float newValue)
	{
		VolumeSettings.ChangeSFXVolumeSetting(newValue);
	}
	public void ChangeMusicVolumeSetting(float newValue)
	{
		VolumeSettings.ChangeMusicVolumeSetting(newValue);
	}
	private void Start()
	{
		GameObject.Find("MainVolume").GetComponent<Slider>().SetValueWithoutNotify(PlayerPrefs.GetInt("MasterVolume"));
		GameObject.Find("SFXVolume").GetComponent<Slider>().SetValueWithoutNotify(PlayerPrefs.GetInt("SFXVolume"));
		GameObject.Find("MusicVolume").GetComponent<Slider>().SetValueWithoutNotify(PlayerPrefs.GetInt("MusicVolume"));
	}
}
