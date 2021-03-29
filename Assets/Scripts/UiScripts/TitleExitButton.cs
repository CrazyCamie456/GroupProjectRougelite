using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleExitButton : MonoBehaviour
{

    public void TaskOnClick()
    {

        StartCoroutine(SceneTransitionFade.LoadNextScene(0));
    }

}
