using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static RoomData;

public class FloorData : MonoBehaviour
{
	public List<string> specialRooms;
	public int minRooms;
	public int maxRooms;
	public int minRoomsBeforeBoss;
	public int maxRoomsBeforeBoss;

	public enum OpeningLocations
	{
		topLeft,
		topRight,
		bottomLeft,
		bottomRight,
		left,
		right
	}

	public OpeningLocations GetOpeningLocation(RoomData.DoorLocations dl)
	{
		switch (dl)
		{
			// @TODO: Add the rest of the door locations here.
			case RoomData.DoorLocations.topLeft_Top_Left:
				return OpeningLocations.topLeft;
			case RoomData.DoorLocations.topLeft_Top_Right:
				return OpeningLocations.topRight;
			case RoomData.DoorLocations.topLeft_Bottom_Left:
				return OpeningLocations.bottomLeft;
			case RoomData.DoorLocations.topLeft_Bottom_Right:
				return OpeningLocations.bottomRight;
			case RoomData.DoorLocations.topLeft_Left:
				return OpeningLocations.left;
			case RoomData.DoorLocations.topLeft_Right:
				return OpeningLocations.right;
		}

		return 0;
	}

	Dictionary<OpeningLocations, List<Tuple<GameObject, RoomData>>> spawnableRooms;
	public List<Tuple<GameObject, RoomData>> rooms;
	List<Vector2Int> roomSpacePositions;

	public GameObject InstantiateRoom(UnityEngine.Object room, Vector2 position)
	{
		return Instantiate(room, new Vector3(position.x, position.y, 0.0f), Quaternion.identity) as GameObject;
	}

	public Vector2 OpeningLocationToVec2(OpeningLocations ol)
	{
		switch (ol)
		{
			case OpeningLocations.topLeft:
			case OpeningLocations.topRight:
				return new Vector2(0.0f, 9.0f);
			case OpeningLocations.bottomLeft:
			case OpeningLocations.bottomRight:
				return new Vector2(0.0f, -9.0f);
			case OpeningLocations.left:
				return new Vector2(-16.0f, 0.0f);
			case OpeningLocations.right:
				return new Vector2(16.0f, 0.0f);
			default: return Vector2.zero;
		}
	}

	public OpeningLocations GetOppositeOpeningLocation(OpeningLocations ol)
	{
		switch (ol)
		{
			case OpeningLocations.topLeft:
				return OpeningLocations.bottomLeft;
			case OpeningLocations.topRight:
				return OpeningLocations.bottomRight;
			case OpeningLocations.bottomLeft:
				return OpeningLocations.topLeft;
			case OpeningLocations.bottomRight:
				return OpeningLocations.topRight;
			case OpeningLocations.left:
				return OpeningLocations.right;
			case OpeningLocations.right:
				return OpeningLocations.left;
			default: return 0;
		}
	}

	Tuple<int, int> GetDoorLocationsRange(OpeningLocations ol)
	{
		switch (ol)
		{
			case OpeningLocations.topLeft:
				return new Tuple<int, int>(0, DOOR_ROT_CODE_TOP_LEFT_WALL_OFFSET);
			case OpeningLocations.topRight:
				return new Tuple<int, int>(DOOR_ROT_CODE_TOP_LEFT_WALL_OFFSET, DOOR_ROT_CODE_TOP_RIGHT_WALL_OFFSET);
			case OpeningLocations.bottomLeft:
				return new Tuple<int, int>(DOOR_ROT_CODE_TOP_RIGHT_WALL_OFFSET, DOOR_ROT_CODE_BOTTOM_LEFT_WALL_OFFSET);
			case OpeningLocations.bottomRight:
				return new Tuple<int, int>(DOOR_ROT_CODE_BOTTOM_LEFT_WALL_OFFSET, DOOR_ROT_CODE_BOTTOM_RIGHT_WALL_OFFSET);
			case OpeningLocations.right:
				return new Tuple<int, int>(DOOR_ROT_CODE_BOTTOM_RIGHT_WALL_OFFSET, DOOR_ROT_CODE_RIGHT_WALL_OFFSET);
			case OpeningLocations.left:
				return new Tuple<int, int>(DOOR_ROT_CODE_RIGHT_WALL_OFFSET, DOOR_ROT_CODE_LEFT_WALL_OFFSET);
			default: return new Tuple<int, int>(0, 0);
		}
	}

	bool CreateNewRoom(Tuple<GameObject, RoomData> attachedTo)
	{
		int attemptsToSpawnRoomLimit = 1000;

		while (attemptsToSpawnRoomLimit > 0)
		{
			// Decide where to put new room
			DoorLocations dl = attachedTo.Item2.validDoorLocations[UnityEngine.Random.Range(0, attachedTo.Item2.validDoorLocations.Count)];

			// Ensure location of new room is valid
			Vector2Int nextRoomPos = attachedTo.Item2.roomSpacePosition;
			if ((int)dl < DOOR_ROT_CODE_TOP_WALL_OFFSET)
				nextRoomPos += Vector2Int.up;
			else if ((int)dl < DOOR_ROT_CODE_BOTTOM_WALL_OFFSET)
				nextRoomPos += Vector2Int.down;
			else if ((int)dl < DOOR_ROT_CODE_RIGHT_WALL_OFFSET)
				nextRoomPos += Vector2Int.right;
			else if ((int)dl < DOOR_ROT_CODE_LEFT_WALL_OFFSET)
				nextRoomPos += Vector2Int.left;

			if (roomSpacePositions.Contains(nextRoomPos))
			{
				attemptsToSpawnRoomLimit--;
				continue;
			}

			// Select new room that can go there
			OpeningLocations ol = GetOpeningLocation(dl);
			Tuple<GameObject, RoomData> newRoomData = spawnableRooms[ol][UnityEngine.Random.Range(0, spawnableRooms[ol].Count)];

			// Find valid door location
			Tuple<int, int> range = GetDoorLocationsRange(GetOppositeOpeningLocation(ol));

			List<DoorLocations> validDoorLocations = new List<DoorLocations>();
			foreach (DoorLocations d in newRoomData.Item2.validDoorLocations)
			{
				if ((int)d >= range.Item1 && (int)d < range.Item2)
					validDoorLocations.Add(d);
			}
			if (validDoorLocations.Count == 0)
			{
				attemptsToSpawnRoomLimit--;
				continue;
			}

			// Instantiate new room
			GameObject newRoom = InstantiateRoom(newRoomData.Item1, nextRoomPos * new Vector2(16.0f, 9.0f));
			RoomData newRoomRoomData = newRoom.GetComponent<RoomData>();

			// Attach doors to new room


			int randomInt = UnityEngine.Random.Range(0, validDoorLocations.Count);
			DoorLocations selectedDoorLocation = validDoorLocations[randomInt];

			attachedTo.Item2.AddDoor(attachedTo.Item1.transform, dl, newRoomRoomData);
			newRoomRoomData.AddDoor(newRoom.transform, selectedDoorLocation, attachedTo.Item2);

			// Add room to rooms list
			rooms.Add(new Tuple<GameObject, RoomData>(newRoom, newRoomRoomData));

			// Return new room
			newRoomRoomData.roomSpacePosition = nextRoomPos;
			roomSpacePositions.Add(nextRoomPos);
			return true;
		}
		return false;
	}

	bool CreateNewRoom(Tuple<GameObject, RoomData> attachedTo, Tuple<GameObject, RoomData> newRoomData)
	{
		int attemptsToSpawnRoomLimit = 1000;

		while (attemptsToSpawnRoomLimit > 0)
		{
			// Decide where to put new room
			DoorLocations dl = attachedTo.Item2.validDoorLocations[UnityEngine.Random.Range(0, attachedTo.Item2.validDoorLocations.Count)];
			if (newRoomData.Item2.validDoorLocations.TrueForAll((x) => GetOppositeOpeningLocation(GetOpeningLocation(x)) != GetOpeningLocation(dl)))
			{
				attemptsToSpawnRoomLimit--;
				continue;
			}

			// Ensure location of new room is valid
			Vector2Int nextRoomPos = attachedTo.Item2.roomSpacePosition;
			if ((int)dl < DOOR_ROT_CODE_TOP_WALL_OFFSET)
				nextRoomPos += Vector2Int.up;
			else if ((int)dl < DOOR_ROT_CODE_BOTTOM_WALL_OFFSET)
				nextRoomPos += Vector2Int.down;
			else if ((int)dl < DOOR_ROT_CODE_RIGHT_WALL_OFFSET)
				nextRoomPos += Vector2Int.right;
			else if ((int)dl < DOOR_ROT_CODE_LEFT_WALL_OFFSET)
				nextRoomPos += Vector2Int.left;

			if (roomSpacePositions.Contains(nextRoomPos))
			{
				attemptsToSpawnRoomLimit--;
				continue;
			}

			OpeningLocations ol = GetOpeningLocation(dl);

			// Move new room
			newRoomData.Item1.transform.position = new Vector3(nextRoomPos.x * 16.0f, nextRoomPos.y * 9.0f, 0.0f);

			// Attach doors to new room
			Tuple<int, int> range = GetDoorLocationsRange(GetOppositeOpeningLocation(ol));

			List<DoorLocations> validDoorLocations = new List<DoorLocations>();
			foreach (DoorLocations d in newRoomData.Item2.validDoorLocations)
			{
				if ((int)d >= range.Item1 && (int)d < range.Item2)
					validDoorLocations.Add(d);
			}
			if (validDoorLocations.Count == 0)
			{
				attemptsToSpawnRoomLimit--;
				continue;
			}

			int randomInt = UnityEngine.Random.Range(0, validDoorLocations.Count);
			DoorLocations selectedDoorLocation = validDoorLocations[randomInt];

			attachedTo.Item2.AddDoor(attachedTo.Item1.transform, dl, newRoomData.Item2);
			newRoomData.Item2.AddDoor(newRoomData.Item1.transform, selectedDoorLocation, attachedTo.Item2);

			// Add room to rooms list
			rooms.Add(newRoomData);

			// Return new room
			newRoomData.Item2.roomSpacePosition = nextRoomPos;
			roomSpacePositions.Add(nextRoomPos);
			return true;
		}
		return false;
	}

	void Start()
	{
		/* tl;dr for why the code below is so weird:
			"Why can't you just be normal"
			LoadAll returning random garbage in whatever unique format it wants: *Screams*
		*/

		roomSpacePositions = new List<Vector2Int>();
		rooms = new List<Tuple<GameObject, RoomData>>();

		// Select an amount of rooms before the boss, and an amount of rooms total.
		int noOfRoomsBeforeBoss;
		if (maxRoomsBeforeBoss < minRoomsBeforeBoss)
			noOfRoomsBeforeBoss = minRoomsBeforeBoss;
		else noOfRoomsBeforeBoss = UnityEngine.Random.Range(minRoomsBeforeBoss, maxRoomsBeforeBoss + 1);
		int noOfRooms = Math.Max(UnityEngine.Random.Range(minRooms, maxRooms + 1), noOfRoomsBeforeBoss);

		// Create a dictionary of spawnable rooms, sorted based on valid door locations
		// Initialise all of the internal lists.
		spawnableRooms = new Dictionary<OpeningLocations, List<Tuple<GameObject, RoomData>>>
		{
			[OpeningLocations.topLeft] = new List<Tuple<GameObject, RoomData>>(),
			[OpeningLocations.topRight] = new List<Tuple<GameObject, RoomData>>(),
			[OpeningLocations.bottomLeft] = new List<Tuple<GameObject, RoomData>>(),
			[OpeningLocations.bottomRight] = new List<Tuple<GameObject, RoomData>>(),
			[OpeningLocations.left] = new List<Tuple<GameObject, RoomData>>(),
			[OpeningLocations.right] = new List<Tuple<GameObject, RoomData>>()
		};

		// Load in all of the rooms as UnityObjects
		UnityEngine.Object[] tempUOs = Resources.LoadAll<GameObject>("Rooms");

		// For each of these rooms, get their GameObject and RoomData, then add them to the applicable lists in SpawnableRooms
		foreach (UnityEngine.Object tempObj in tempUOs)
		{
			GameObject tempGO = (GameObject)tempObj;
			RoomData tempRD = tempGO.GetComponent<RoomData>();

			foreach (DoorLocations d in tempRD.validDoorLocations)
			{
				spawnableRooms[GetOpeningLocation(d)].Add(new Tuple<GameObject, RoomData>(tempGO, tempRD));
			}
		}

		// Create the starting room
		GameObject startingRoom = InstantiateRoom(Resources.Load("SpecialRooms/StartingRoom"), new Vector2(0.0f, 0.0f));
		startingRoom.name = "StartingRoom";
		noOfRoomsBeforeBoss--;
		noOfRooms--;
		RoomData srRd = startingRoom.GetComponent<RoomData>();
		rooms.Add(new Tuple<GameObject, RoomData>(startingRoom, srRd));
		srRd.roomSpacePosition = Vector2Int.zero;
		roomSpacePositions.Add(Vector2Int.zero);

		// Build a line of rooms leading to the boss, these will be branched off from later.
		while (noOfRoomsBeforeBoss > 1)
		{
			CreateNewRoom(rooms[rooms.Count - 1]);

			noOfRoomsBeforeBoss--;
			noOfRooms--;
		}

		GameObject bossRoom = InstantiateRoom(Resources.Load("SpecialRooms/BossRoom"), Vector2.zero);
		RoomData bossRoomRoomData = bossRoom.GetComponent<RoomData>();
		bool success = false;
		while (!success)
		{
			success = CreateNewRoom(rooms[rooms.Count - 1], new Tuple<GameObject, RoomData>(bossRoom, bossRoomRoomData));
			if (success)
				rooms[rooms.Count - 1].Item1.name = "BossRoom";

			if (!success)
			{
				CreateNewRoom(rooms[rooms.Count - 1]);
				noOfRooms--;
			}
			if (noOfRooms < -4)
			{
				Debug.LogError("Attempt to spawn floor failed.");
				Debug.Break();
			}
		}

		noOfRooms--;

		while (noOfRooms > 0)
		{
			// If a random number is less than the amount of special rooms left / the number of rooms left.
			bool spawnSpecialRoom = UnityEngine.Random.Range(0.0f, 0.999f) < (specialRooms.Count / noOfRooms);
			success = false;

			int tentativeRoom = UnityEngine.Random.Range(0, rooms.Count);
			while (rooms[tentativeRoom].Item1.name == "BossRoom")
				tentativeRoom = UnityEngine.Random.Range(0, rooms.Count);

			if (spawnSpecialRoom)
			{
				int whichIndex = UnityEngine.Random.Range(0, specialRooms.Count);
				GameObject specialRoom = InstantiateRoom(Resources.Load("SpecialRooms/" + specialRooms[whichIndex]), new Vector2(0.0f, 0.0f));
				RoomData specialRoomRD = specialRoom.GetComponent<RoomData>();
				while (!success)
				{
					Tuple<GameObject, RoomData> rd = new Tuple<GameObject, RoomData>(specialRoom, specialRoomRD);
					success = CreateNewRoom(rooms[tentativeRoom], rd);
					if (success)
						specialRooms.RemoveAt(whichIndex);

					// Select new room to attempt to build off of - in case something has gone wrong.
					tentativeRoom = UnityEngine.Random.Range(0, rooms.Count);
					while (rooms[tentativeRoom].Item1.name == "BossRoom")
						tentativeRoom = UnityEngine.Random.Range(0, rooms.Count);
				}
			}
			else
			{
				while (!success)
				{
					success = CreateNewRoom(rooms[tentativeRoom]);
					tentativeRoom = UnityEngine.Random.Range(0, rooms.Count);
					while (rooms[tentativeRoom].Item1.name == "BossRoom")
						tentativeRoom = UnityEngine.Random.Range(0, rooms.Count);
				}
			}
			noOfRooms--;
		}

		AstarPath.active.Scan();

		// @TODO: Link some created rooms to each other, if not already linked - create more possible paths through a floor.
	}
}
