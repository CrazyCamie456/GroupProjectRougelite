﻿using System;
using System.Collections;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
	Rigidbody2D playerRB;
	CombatStats playerCS;
	SpriteRenderer sr;

	CameraController cc;

	static Guid roomChangeCC = new Guid();

	public Vector2 nextRoomDirection;
	private Vector2 roomSize = new Vector2(16.0f, 9.0f);
	public RoomManager nextRoom;
	public bool isDoorLocked
	{
		get
		{
			return pIsDoorLocked;
		}
		set
		{
			sr.sprite = value ? closed : open;
			pIsDoorLocked = value;
		}
	}
	private bool pIsDoorLocked;
	public Sprite open;
	public Sprite closed;
	static float lastDoorTime;

	void Awake()
	{
		sr = GetComponent<SpriteRenderer>();
	}

	void Start()
	{
		cc = Camera.main.GetComponent<CameraController>();
		GameObject temp = GameObject.Find("Player");
		playerRB = temp.GetComponent<Rigidbody2D>();
		playerCS = temp.GetComponent<CombatStats>();
	}

	IEnumerator DoorPlayerSlide()
	{
		Time.timeScale = 0.0f;
		Vector3 nextRoomDir3 = new Vector3(nextRoomDirection.x, nextRoomDirection.y, 0.0f);
		float elapsedTime = 0.0f;
		playerCS.ApplyCrowdControl(roomChangeCC);
		playerRB.simulated = false;
		while (elapsedTime < 1.0f)
		{
			playerCS.transform.position += nextRoomDir3 * 3.14f * Time.unscaledDeltaTime;
			if (elapsedTime < 0.5f)
				playerCS.transform.localScale -= playerCS.transform.localScale * Time.unscaledDeltaTime;
			else
				playerCS.transform.localScale += playerCS.transform.localScale * Time.unscaledDeltaTime;
			elapsedTime += Time.unscaledDeltaTime;
			yield return null;
		}
		// Just to be certain
		playerCS.transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
		playerCS.RemoveCrowdControl(roomChangeCC);
		playerRB.simulated = true;
		GetComponentInParent<RoomManager>().ExitRoom();
		Time.timeScale = 1.0f;
	}

	void OnTriggerStay2D(Collider2D collision)
	{
		// This just needs to guarantee the player doesn't exit a room and return into it the next frame.
		if (collision.tag == "Player" && !isDoorLocked && Time.time - lastDoorTime > Time.deltaTime + 0.00001f)
		{
			cc.targetPosition += nextRoomDirection * roomSize;
			lastDoorTime = Time.time;
			nextRoom.EnterRoom();
			StartCoroutine(DoorPlayerSlide());
		}
	}
}