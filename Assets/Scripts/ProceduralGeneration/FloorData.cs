using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorData : MonoBehaviour
{
	RoomData roomData;

	public List<string> specialRooms;
	public int minRooms;
	public int maxRooms;
	public int minRoomsBeforeBoss;
	public int maxRoomsBeforeBoss;

	List<GameObject> spawnableRooms;
	List<RoomData> spawnableRoomsRoomData;
	public List<GameObject> rooms;

	public GameObject CreateRoom(UnityEngine.Object room, Vector2 position)
	{
		return Instantiate(room, new Vector3(position.x, position.y, 0.0f), Quaternion.identity) as GameObject;
	}

	public GameObject CreateRoom(int index, Vector2 position)
	{
		return Instantiate(spawnableRooms[index], new Vector3(position.x, position.y, 0.0f), Quaternion.identity);
	}

	void Start()
	{
		/* tl;dr for why the code below is so weird:
			"Why can't you just be normal"
			LoadAll returning random garbage in whatever unique format it wants: *Screams*
		*/

		int noOfRoomsBeforeBoss;
		if (maxRoomsBeforeBoss < minRoomsBeforeBoss)
			noOfRoomsBeforeBoss = minRoomsBeforeBoss;
		else noOfRoomsBeforeBoss = UnityEngine.Random.Range(minRoomsBeforeBoss, maxRoomsBeforeBoss + 1);
		int noOfRooms = Math.Max(UnityEngine.Random.Range(minRooms, maxRooms + 1), noOfRoomsBeforeBoss);

		spawnableRooms = new List<GameObject>();
		UnityEngine.Object[] temp = Resources.LoadAll<GameObject>("Rooms");
		foreach (UnityEngine.Object tempObj in temp)
			spawnableRooms.Add((GameObject)tempObj);

		spawnableRoomsRoomData = new List<RoomData>();
		foreach (GameObject g in spawnableRooms)
			spawnableRoomsRoomData.Add(g.GetComponent<RoomData>());

		GameObject startingRoom = CreateRoom(Resources.Load("SpecialRooms/StartingRoom"), new Vector2(0.0f, 0.0f));
		startingRoom.name = "StartingRoom";
		noOfRoomsBeforeBoss--;
		RoomManager srRm = startingRoom.GetComponent<RoomManager>();
		rooms.Add(startingRoom);

		GameObject testRoom = CreateRoom(temp[0], new Vector2(16.0f, 9.0f));
		RoomManager trRm = testRoom.GetComponent<RoomManager>();

		while (noOfRoomsBeforeBoss > 0)
		{
			noOfRoomsBeforeBoss--;
			noOfRooms--;
		}
		while (noOfRooms > 0)
		{
			noOfRooms--;
		}
	}
}
