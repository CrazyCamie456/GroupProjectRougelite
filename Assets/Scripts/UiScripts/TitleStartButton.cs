using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleStartButton : MonoBehaviour
{

    public void TaskOnClick()
    {
        StartCoroutine(SceneTransitionFade.LoadNextScene((int)GameScenes.ByID.inGame));
    }


}
