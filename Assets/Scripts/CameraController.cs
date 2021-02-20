using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
	public Vector2 targetPosition;
	public float movementSpeed;

	void Update()
    {
		transform.position = Vector2.Lerp(transform.position, targetPosition, Time.deltaTime * movementSpeed);
		transform.position = new Vector3(transform.position.x, transform.position.y, -10.0f);
    }
}
