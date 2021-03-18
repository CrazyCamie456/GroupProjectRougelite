using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Death : MonoBehaviour
{
	static bool isQuitting = false;

	// This solves the bug with gameObjects (corpses) being created when the game is quit in the editor.
	[RuntimeInitializeOnLoadMethod] static void RunOnStart() { Application.quitting += () => isQuitting = true; }

	private void OnDestroy()
    {
		if (isQuitting) return;

		GameObject g = Instantiate(Resources.Load("Prefabs/FishmanDying"), transform.position, Quaternion.identity) as GameObject;
		g.GetComponent<SpriteRenderer>().flipX = GetComponentInChildren<SpriteRenderer>().flipX;
	}
}
