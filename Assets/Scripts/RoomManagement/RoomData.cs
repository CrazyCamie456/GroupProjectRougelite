using System.Collections.Generic;
using UnityEngine;

public class RoomData : MonoBehaviour // This is only a MonoBehaviour because it should show in the inspector, if you're using Start or Update, etc. in here, please reconsider.
{
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
		topLeft_Top_Left = 0, topLeft_Top_Center, topLeft_Top_Right, // Along the top
		topLeft_Right_Top = 12, topLeft_Right_Center, topLeft_Right_Bottom, // Along the right
		topLeft_Bottom_Left = 24, topLeft_Bottom_Center, topLeft_Bottom_Right, // Along the bottom
		topLeft_Left_Top = 36, topLeft_Left_Center, topLeft_Left_Bottom, // Along the left

		__ = -1,

		// **************
		// Top Right Room
		topRight_Top_Left = 3, topRight_Top_Center, topRight_Top_Right, // Along the top
		topRight_Right_Top = 15, topRight_Right_Center, topRight_Right_Bottom, // Along the right
		topRight_Bottom_Left = 27, topRight_Bottom_Center, topRight_Bottom_Right, // Along the bottom
		topRight_Left_Top = 39, topRight_Left_Center, topRight_Left_Bottom, // Along the left

		___ = -1,

		// **************
		// Bottom Left Room
		bottomLeft_Top_Left = 6, bottomLeft_Top_Center, bottomLeft_Top_Right, // Along the top
		bottomLeft_Right_Top = 18, bottomLeft_Right_Center, bottomLeft_Right_Bottom, // Along the right
		bottomLeft_Bottom_Left = 30, bottomLeft_Bottom_Center, bottomLeft_Bottom_Right, // Along the bottom
		bottomLeft_Left_Top = 42, bottomLeft_Left_Center, bottomLeft_Left_Bottom, // Along the left

		____ = -1,

		// **************
		// Bottom Right Room
		bottomRight_Top_Left = 9, bottomRight_Top_Center, bottomRight_Top_Right, // Along the top
		bottomRight_Right_Top = 21, bottomRight_Right_Center, bottomRight_Right_Bottom, // Along the right
		bottomRight_Bottom_Left = 33, bottomRight_Bottom_Center, bottomRight_Bottom_Right, // Along the bottom
		bottomRight_Left_Top = 45, bottomRight_Left_Center, bottomRight_Left_Bottom // Along the left
	}
	/*	The below variables are used to ensure that doors are rotated correctly, and that
		they move the player in the correct direction so that they end up in the correct room.
		IF YOU DON'T UNDERSTAND WHAT THEY DO, DON'T CHANGE THEM. IF YOU NEED TO CHANGE THEM, BE SURE THEY ARE CORRECT
	   
		Also, you probably don't need to access these.
	*/
	public readonly int DOOR_ROT_CODE_DOORS_PER_SIDE = 12;
	public readonly int DOOR_ROT_CODE_TOP_WALL_OFFSET = 0;
	public readonly int DOOR_ROT_CODE_RIGHT_WALL_OFFSET = 12;
	public readonly int DOOR_ROT_CODE_BOTTOM_WALL_OFFSET = 24;
	public readonly int DOOR_ROT_CODE_LEFT_WALL_OFFSET = 36;

	// *************************************
	// VERY IMPORTANT ENUM STUFF ENDS HERE
	// *************************************
	#endregion

	public readonly int maximumDoors;

	public List<DoorLocations> validDoorLocations;

	[HideInInspector] public List<DoorLocations> doorLocations;

	GameObject doorGameObject;

	Vector3 DoorLocationToRelativeWorldPosition(DoorLocations dl)
	{
		return Vector3.zero;
	}

	void AddDoor(DoorLocations dl, GameObject leadsTo)
	{
		doorLocations.Add(dl);
		Instantiate(doorGameObject);
	}

	public RoomManager rm;

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
}