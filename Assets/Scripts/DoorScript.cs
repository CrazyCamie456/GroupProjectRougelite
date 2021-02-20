using System;
using System.Collections;
using UnityEngine;

public class DoorScript : MonoBehaviour
{
	Rigidbody2D playerRB;
	CombatStats playerCS;

	CameraController cc;

	static Guid roomChangeCC = new Guid();

	public Vector2 currentRoomTargetPos;
	public Vector2 otherRoomTargetPos;
	public float maxDoorLockout = 1.0f;
	float doorLockout;
	public bool isDoorLocked;
	public Vector3 slideDir = new Vector3(4.0f, 0.0f, 0.0f);

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
		float elapsedTime = 0.0f;
		playerCS.ApplyCrowdControl(roomChangeCC);
		playerRB.simulated = false;
		while (elapsedTime < 1.0f)
		{
			playerCS.transform.position += slideDir * Time.deltaTime;
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
			cc.targetPosition = otherRoomTargetPos;
			Vector2 temp = otherRoomTargetPos;
			otherRoomTargetPos = currentRoomTargetPos;
			currentRoomTargetPos = temp;
			StartCoroutine(DoorLockoutTimer());
			StartCoroutine(DoorPlayerSlide());
		}
	}
}
