using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public static class SceneTransitionFade
{
    public static Animator transitioner = GameObject.Find("Crossfade To Black").GetComponentInChildren<Animator>();
    public static float transitionTime = 1.0f;
    
    public static IEnumerator LoadNextScene(int levelIndex)
    {
        transitioner.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        SceneManager.LoadScene(levelIndex);
    }

}