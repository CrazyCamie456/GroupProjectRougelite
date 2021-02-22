using System;
using System.Collections;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
	Rigidbody2D playerRB;
	CombatStats playerCS;

	CameraController cc;

	static Guid roomChangeCC = new Guid();

	public Vector2 nextRoomDirection;
	private Vector2 roomSize = new Vector2(16.0f, 9.0f);
	public float maxDoorLockout = 1.0f;
	float doorLockout;
	public bool isDoorLocked;

	IEnumerator DoorLockoutTimer()
	{
		doorLockout = maxDoorLockout;
		while (doorLockout > 0.0f) {
			doorLockout -= Time.deltaTime;
			yield return null;
		}
	}

	void Start()
	{
		cc = Camera.main.GetComponent<CameraController>();
		doorLockout = -1.0f;

		GameObject temp = GameObject.Find("Player");
		playerRB = temp.GetComponent<Rigidbody2D>();
		playerCS = temp.GetComponent<CombatStats>();
	}

	IEnumerator DoorPlayerSlide()
	{
		Vector3 nextRoomDir3 = new Vector3(nextRoomDirection.x, nextRoomDirection.y, 0.0f);
		float elapsedTime = 0.0f;
		playerCS.ApplyCrowdControl(roomChangeCC);
		playerRB.simulated = false;
		while (elapsedTime < 1.0f)
		{
			playerCS.transform.position += nextRoomDir3 * 3.0f * Time.deltaTime;
			if (elapsedTime < 0.5f)
				playerCS.transform.localScale -= playerCS.transform.localScale * Time.deltaTime;
			else
				playerCS.transform.localScale += playerCS.transform.localScale * Time.deltaTime;
			elapsedTime += Time.deltaTime;
			yield return null;
		}
		playerCS.transform.localScale = new Vector3(1.0f, 1.0f, 0.0f);
		playerCS.RemoveCrowdControl(roomChangeCC);
		playerRB.simulated = true;
	}

	void OnTriggerEnter2D(Collider2D collision)
	{
		if (collision.tag == "Player" && doorLockout < 0.0f && !isDoorLocked)
		{
			cc.targetPosition += nextRoomDirection * roomSize;
			StartCoroutine(DoorLockoutTimer());
			StartCoroutine(DoorPlayerSlide());
		}
	}
}
