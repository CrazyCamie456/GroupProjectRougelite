using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LinkedDestroy : MonoBehaviour
{
	public List<GameObject> otherObjects;

	void Start()
	{
		if (otherObjects == null)
			otherObjects = new List<GameObject>();
	}

	void OnDestroy()
	{
		otherObjects.ForEach((GameObject g) => Destroy(g));
	}
}
