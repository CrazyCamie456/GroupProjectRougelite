using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class TitleStartButton : MonoBehaviour
{
    public Animator transitioner;
    public float transitionTime = 1.0f;
    public void TaskOnClick()
    {
        StartCoroutine(LoadNextScene((int)GameScenes.ByID.inGame));
    }

    IEnumerator LoadNextScene(int levelIndex)
    {
        transitioner.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }
}
