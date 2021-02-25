using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomUnlocker : MonoBehaviour
{
	public List<GameObject> checks;
	Coroutine cr;

	IEnumerator UnlockRoomOnComplete()
	{
		while (checks.Exists((g) => g != null))
		{
			yield return null;
		}
		GetComponent<RoomManager>().UnlockAll();
	}

	public void Start()
	{
		// This is assigned to a variable to ensure the same coroutine is not run multiple times, and to override the previous one if it is.
		if (cr != null) StopCoroutine(cr);
		cr = StartCoroutine(UnlockRoomOnComplete());
	}
}
