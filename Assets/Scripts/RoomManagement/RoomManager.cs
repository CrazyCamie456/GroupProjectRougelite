using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
	[SerializeField] private DoorScript[] doors;
	public IRoomUnlocker ru;
	[SerializeField] private bool isCurrentRoom
	{
		get { return pIsCurrentRoom; }
		set
		{
			if (value) EnterRoom();
			else ExitRoom();
			pIsCurrentRoom = value;
		}
	}
	bool pIsCurrentRoom;

	public void UnlockAll()
	{
		foreach (DoorScript d in doors)
			d.isDoorLocked = false;
	}

	public void LockAll()
	{
		foreach (DoorScript d in doors)
			d.isDoorLocked = true;
	}

	public void EnterRoom()
	{
		Transform[] temp = GetComponentsInChildren<Transform>(true);
		foreach (Transform t in temp)
		{
			t.gameObject.SetActive(true);
		}
	}

	public void ExitRoom()
	{
		Transform[] temp = GetComponentsInChildren<Transform>(false);
		foreach (Transform t in temp)
		{
			if (t == transform)
				t.gameObject.SetActive(true);
			else
				t.gameObject.SetActive(false);
		}
	}

	void Initialise(bool _isCurrentRoom)
	{
		isCurrentRoom = _isCurrentRoom;
	}

	void Start()
	{
		ru = GetComponent<IRoomUnlocker>();
		doors = GetComponentsInChildren<DoorScript>();
		isCurrentRoom = gameObject.name == "StartingRoom";
		if (isCurrentRoom)
			LockAll();
	}
}
