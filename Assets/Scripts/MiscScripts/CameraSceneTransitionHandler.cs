using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSceneTransitionHandler : MonoBehaviour
{
    private void Start()
    {
        SceneTransitionFade.transitioner = GameObject.Find("Crossfade To Black").GetComponentInChildren<Animator>();
    }

    public void PlayerDeathSceneTransition()
    {
        StartCoroutine(SceneTransitionFade.LoadNextScene((int)GameScenes.ByID.deathScreen));
    }
}
