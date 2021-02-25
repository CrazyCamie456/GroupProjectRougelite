using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomManager : MonoBehaviour
{
	[SerializeField] private DoorScript[] doors;
	[SerializeField] private RoomUnlocker ru;
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
		LockAll();
		// This starts a coroutine that will reunlock the room if the condition has already been met.
		if (ru != null)
			ru.Start();
		else UnlockAll();
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
		TryGetComponent(out ru);
		doors = GetComponentsInChildren<DoorScript>();
		isCurrentRoom = gameObject.name == "StartingRoom";
	}

	void Update()
	{

	}
}
