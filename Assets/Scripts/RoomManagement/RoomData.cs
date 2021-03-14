using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour // This is only a MonoBehaviour because it should show in the inspector, if you're using Start or Update, etc. in here, please reconsider.
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
		topLeft_Top_Left = 0, topLeft_Top_Right, // Along the top
		topLeft_Bottom_Left = 8, topLeft_Bottom_Right, // Along the bottom
		topLeft_Right = 16,
		topLeft_Left = 20,

		__ = -1,

		// **************
		// Top Right Room
		topRight_Top_Left = 2, topRight_Top_Right, // Along the top
		topRight_Bottom_Left = 10, topRight_Bottom_Right, // Along the bottom
		topRight_Right = 17, 
		topRight_Left = 21,

		___ = -1,

		// **************
		// Bottom Left Room
		bottomLeft_Top_Left = 4, bottomLeft_Top_Right, // Along the top
		bottomLeft_Bottom_Left = 12, bottomLeft_Bottom_Right, // Along the bottom
		bottomLeft_Right = 18,// Along the right
		bottomLeft_Left = 22, // Along the left

		____ = -1,

		// **************
		// Bottom Right Room
		bottomRight_Top_Left = 6, bottomRight_Top_Right, // Along the top
		bottomRight_Bottom_Left = 14, bottomRight_Bottom_Right, // Along the bottom
		bottomRight_Right = 19, // Along the right
		bottomRight_Left = 23 // Along the left
	}
	/*	The below variables are used to ensure that doors are rotated correctly, and that
		they move the player in the correct direction so that they end up in the correct room.
		IF YOU DON'T UNDERSTAND WHAT THEY DO, DON'T CHANGE THEM. IF YOU NEED TO CHANGE THEM, BE SURE THEY ARE CORRECT
	   
		Also, you probably don't need to access these.
	*/
	public readonly int DOOR_ROT_CODE_TOP_WALL_OFFSET = 0;
	public readonly int DOOR_ROT_CODE_BOTTOM_WALL_OFFSET = 8;
	public readonly int DOOR_ROT_CODE_RIGHT_WALL_OFFSET = 16;
	public readonly int DOOR_ROT_CODE_LEFT_WALL_OFFSET = 20;

	// *************************************
	// VERY IMPORTANT ENUM STUFF ENDS HERE
	// *************************************
	#endregion

	public readonly int maximumDoors;

	public List<DoorLocations> validDoorLocations;

	private void OnDrawGizmos()
	{
		Gizmos.color = Color.red;
		Vector3 pos = new Vector3();
		Vector3 size = new Vector3(1.0f, 1.0f, 0.0f);
		foreach (DoorLocations d in validDoorLocations)
		{
			pos = DoorLocationToRelativeVec3(d);
			Gizmos.DrawWireCube(pos, size);
		}
	}

	Vector3 DoorLocationToRelativeVec3(DoorLocations d)
	{
		switch (d)
		{
			case DoorLocations.topLeft_Top_Left:		return new Vector3(-3.5f, -4.0f, 0.0f);
			case DoorLocations.topLeft_Top_Right:		return new Vector3(3.5f, -4.0f, 0.0f); 
			case DoorLocations.topLeft_Bottom_Left:		return new Vector3(-3.5f, 4.0f, 0.0f);
			case DoorLocations.topLeft_Bottom_Right:	return new Vector3(3.5f, 4.0f, 0.0f);
			case DoorLocations.topLeft_Left:			return new Vector3(-7.5f, 0.0f, 0.0f);
			case DoorLocations.topLeft_Right:			return new Vector3(7.5f, 0.0f, 0.0f);
			
			default:									return Vector3.zero;
		}
	}

	[HideInInspector] public List<DoorLocations> doorLocations;

	GameObject doorGameObject;

	Vector3 DoorLocationToRelativeWorldPosition(DoorLocations dl)
	{
		return Vector3.zero;
	}

	void AddDoor(DoorLocations dl, RoomData leadsTo)
	{
		doorLocations.Add(dl);
		Instantiate(doorGameObject, transform.position + DoorLocationToRelativeWorldPosition(dl), Quaternion.identity);

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