using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSceneTransitionHandler : MonoBehaviour
{
    public void PlayerDeathSceneTransition()
    {
        StartCoroutine(SceneTransitionFade.LoadNextScene((int)GameScenes.ByID.deathScreen));
    }
}
