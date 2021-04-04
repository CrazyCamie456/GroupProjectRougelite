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
       StartCoroutine(LoadQuit(0));
    }
    public IEnumerator LoadQuit(int levelIndex)
    {
        transitioner.SetTrigger("Start");
        yield return new WaitForSeconds(transitionTime);
        Application.Quit();
    }
	private void Start()
	{
		transitioner = GameObject.Find("Crossfade To Black").GetComponentInChildren<Animator>();
	}
}
