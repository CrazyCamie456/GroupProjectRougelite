using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ResolutionSettingsUIController : MonoBehaviour
{
	Dropdown dd;

	private void Start()
	{
		dd = GameObject.Find("ResolutionDropdown").GetComponent<Dropdown>();
	}

	public static List<Vector2Int> resolutionOptions = new List<Vector2Int>() {
		new Vector2Int(1280, 720),
		new Vector2Int(1920, 1080),
		new Vector2Int(2560, 1440),
		new Vector2Int(3840, 2160)
	};

	public static int index;
	public static bool isFullscreen = true;

	public void ChangeCurrentResolution(int _index)
	{
		index = _index;
		Screen.SetResolution(resolutionOptions[index].x, resolutionOptions[index].y, isFullscreen);
		PlayerPrefs.SetInt("Resolution", index);
	}

	public void ToggleFullscreen(bool _isFullscreen)
	{
		isFullscreen = _isFullscreen;
		Screen.SetResolution(resolutionOptions[dd.value].x, resolutionOptions[dd.value].y, isFullscreen);
		PlayerPrefs.SetInt("Fullscreen", _isFullscreen ? 1 : 0);
	}
}