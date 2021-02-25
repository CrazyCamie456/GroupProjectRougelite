using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveUpDownOnSine : MonoBehaviour
{
	Rigidbody2D rb;

	float startingY;
	public float yDelta = 1.0f;
	public float timeScale = 1.0f;

    void Start()
    {
		rb = GetComponent<Rigidbody2D>();
		startingY = rb.position.y;
    }

    void Update()
    {
		rb.position = new Vector2(rb.position.x, startingY + (Mathf.Sin(Time.time * timeScale) * yDelta));
    }
}
