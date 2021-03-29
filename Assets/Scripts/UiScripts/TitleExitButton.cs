using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleExitButton : MonoBehaviour
{
    public static Animator transitioner = GameObject.Find("Crossfade To Black").GetComponentInChildren<Animator>();
    public static float transitionTime = 1.0f;
    public void TaskOnClick()
    {

        StartCoroutine(SceneTransitionFade.LoadNextScene(0));
    }
    public static IEnumerator LoadNextScene(int levelIndex)
    {
        transitioner.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        Application.Quit();
    }
}
