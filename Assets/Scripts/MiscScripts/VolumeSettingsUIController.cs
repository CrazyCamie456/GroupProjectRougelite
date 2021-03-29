using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
}
