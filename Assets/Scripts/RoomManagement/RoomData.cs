using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour
{
	public List<DoorScript> doors;
	public IRoomUnlocker ru;
	[SerializeField]
	private bool isCurrentRoom
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

	public enum RoomSize
	{
		small, large, L_shape, J_shape, wide, tall
	}

	public RoomSize roomSize;

	public Vector2Int roomSpacePosition;

	#region DoorLocationsEnum
	// *************************************
	// VERY IMPORTANT ENUM STUFF STARTS HERE
	// *************************************

	/* 
		READ ME: All door locations are stored in the order ( room_side_position )
		EXAMPLE: The door in the middle of the right hand wall of the lower-right room in a J-Shaped room would be bottomRight_Right_Middle

		The _'s are for spacing in the inspector, please don't use them, I mean, the code won't let you, but still.

		Actually Unity serializes these badly anyway, just don't change the damn thing. Like at all.
		Add to the end sure, change no.

		IMPPORTANT: THE '= n' IN THIS ENUM IS IMPORTANT FOR PROCEDURAL GENERATION, IF YOU CHANGE THIS ENUM
					(other than changing names), BE VERY CAREFUL, AND UPDATE THE VARIABLES DIRECTLY BELOW
					If you need help with this enum, just let me know - Camie
	*/
	public enum DoorLocations
	{
		// *************
		// Top Left Room
		topLeft_Top_Left = 0, topLeft_Top_Right = 4, // Along the top
		topLeft_Bottom_Left = 8, topLeft_Bottom_Right = 12, // Along the bottom
		topLeft_Right = 16,
		topLeft_Left = 20,

		__ = -1,

		// **************
		// Top Right Room
		topRight_Top_Left = 1, topRight_Top_Right = 5, // Along the top
		topRight_Bottom_Left = 9, topRight_Bottom_Right = 13, // Along the bottom
		topRight_Right = 17, 
		topRight_Left = 21,

		___ = -1,

		// **************
		// Bottom Left Room
		bottomLeft_Top_Left = 2, bottomLeft_Top_Right = 6, // Along the top
		bottomLeft_Bottom_Left = 10, bottomLeft_Bottom_Right = 14, // Along the bottom
		bottomLeft_Right = 18,// Along the right
		bottomLeft_Left = 22, // Along the left

		____ = -1,

		// **************
		// Bottom Right Room
		bottomRight_Top_Left = 3, bottomRight_Top_Right = 7, // Along the top
		bottomRight_Bottom_Left = 11, bottomRight_Bottom_Right = 15, // Along the bottom
		bottomRight_Right = 19, // Along the right
		bottomRight_Left = 23 // Along the left
	}
	/*	The below variables are used to ensure that doors are rotated correctly, and that
		they move the player in the correct direction so that they end up in the correct room.
		IF YOU DON'T UNDERSTAND WHAT THEY DO, DON'T CHANGE THEM. IF YOU NEED TO CHANGE THEM, BE SURE THEY ARE CORRECT
	   
		Also, you probably don't need to access these.
	*/
	public static readonly int DOOR_ROT_CODE_TOP_LEFT_WALL_OFFSET = 4;
	public static readonly int DOOR_ROT_CODE_TOP_RIGHT_WALL_OFFSET = 8;
	public static readonly int DOOR_ROT_CODE_TOP_WALL_OFFSET = 8;
	public static readonly int DOOR_ROT_CODE_BOTTOM_LEFT_WALL_OFFSET = 12;
	public static readonly int DOOR_ROT_CODE_BOTTOM_RIGHT_WALL_OFFSET = 16;
	public static readonly int DOOR_ROT_CODE_BOTTOM_WALL_OFFSET = 16;
	public static readonly int DOOR_ROT_CODE_RIGHT_WALL_OFFSET = 20;
	public static readonly int DOOR_ROT_CODE_LEFT_WALL_OFFSET = 24;

	// *************************************
	// VERY IMPORTANT ENUM STUFF ENDS HERE
	// *************************************
	#endregion

	public readonly int maximumDoors;

	public List<DoorLocations> validDoorLocations;

	private void OnDrawGizmos()
	{
		if (name == "StartingRoom")
			Gizmos.color = Color.magenta;
		else if (name == "BossRoom")
			Gizmos.color = Color.red; 
		else
			Gizmos.color = Color.blue;
		Vector3 pos = new Vector3(0.0f, 0.0f, 0.0f);
		Vector3 size = new Vector3(15.9f, 8.9f, 0.0f);
		pos = roomSpacePosition * new Vector2(16.0f, 9.0f);
		Gizmos.DrawWireCube(pos, size);

		if (isCurrentRoom)
		{
			Gizmos.color = Color.red;
			pos = new Vector3();
			size = new Vector3(1.0f, 1.0f, 0.0f);
			foreach (DoorLocations d in validDoorLocations)
			{
				pos = transform.position + DoorLocationToRelativeVec3(d);
				Gizmos.DrawWireCube(pos, size);
			}
		}
	}

	Vector3 DoorLocationToRelativeVec3(DoorLocations d)
	{
		switch (d)
		{
			case DoorLocations.topLeft_Top_Left:		return new Vector3(-3.5f, 4.0f, 0.0f);
			case DoorLocations.topLeft_Top_Right:		return new Vector3(3.5f, 4.0f, 0.0f); 
			case DoorLocations.topLeft_Bottom_Left:		return new Vector3(-3.5f, -4.0f, 0.0f);
			case DoorLocations.topLeft_Bottom_Right:	return new Vector3(3.5f, -4.0f, 0.0f);
			case DoorLocations.topLeft_Left:			return new Vector3(-6.5f, 0.0f, 0.0f);
			case DoorLocations.topLeft_Right:			return new Vector3(6.5f, 0.0f, 0.0f);

			// @TODO: Finish adding these
			
			default:									return Vector3.zero;
		}
	}

	Quaternion DoorLocationToRotation(DoorLocations d)
	{
		if ((int)d < DOOR_ROT_CODE_TOP_WALL_OFFSET)
			return Quaternion.Euler(0.0f, 0.0f, 0.0f);
		if ((int)d < DOOR_ROT_CODE_BOTTOM_WALL_OFFSET)
			return Quaternion.Euler(0.0f, 0.0f, 180.0f);
		if ((int)d < DOOR_ROT_CODE_RIGHT_WALL_OFFSET)
			return Quaternion.Euler(0.0f, 0.0f, 270.0f);
		if ((int)d < DOOR_ROT_CODE_LEFT_WALL_OFFSET)
			return Quaternion.Euler(0.0f, 0.0f, 90.0f);

		return Quaternion.identity;
	}

	[HideInInspector] public List<DoorLocations> doorLocations;

	GameObject doorGameObject;

	public void AddDoor(Transform parent, DoorLocations dl, RoomData leadsTo)
	{
		doorLocations.Add(dl);
		GameObject door = Instantiate(doorGameObject, transform.position + DoorLocationToRelativeVec3(dl), DoorLocationToRotation(dl));
		door.GetComponent<DoorScript>().nextRoom = leadsTo;
		door.transform.parent = parent;
	}

	void Awake()
	{
		if (doorGameObject == null)
			doorGameObject = Resources.Load("Prefabs/Door") as GameObject;

		for (int i = 0; i < validDoorLocations.Count; i++)
		{
			if ((int)validDoorLocations[i] == -1)
			{
				throw new System.Exception("You've selected an '_' as a \"valid door location\" on the room called " + gameObject.name + " at index " + i + ".");
			}
		}
	}

	void Start()
	{
		ru = GetComponent<IRoomUnlocker>();
		doors = new List<DoorScript>(GetComponentsInChildren<DoorScript>());
		isCurrentRoom = gameObject.name == "StartingRoom";
		if (isCurrentRoom)
			LockAll();
	}
}