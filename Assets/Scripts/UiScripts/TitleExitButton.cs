using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class TitleExitButton : MonoBehaviour
{
    public Animator transitioner;
    public float transitionTime = 1.0f;
    public void TaskOnClick()
    {

        StartCoroutine(LoadNextScene(0));
    }
    IEnumerator LoadNextScene(int levelIndex)
    {
        transitioner.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        Application.Quit();
    }
}
