using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleOptionsButton : MonoBehaviour
{
    public void TaskOnClick()
    {
        SceneManager.LoadScene((int)GameScenes.ByID.optionsMenu);
    }
}
