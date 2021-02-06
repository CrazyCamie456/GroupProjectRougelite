using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DebugEnemyMovement : MonoBehaviour
{
    private Rigidbody2D rb;
    private Rigidbody2D rbPlayer;


    private Vector2 movementDirection;
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        //Get the player rigidbody
        rbPlayer = GameObject.Find("Player").GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        //move towards the player
        movementDirection = rbPlayer.position - rb.position;
        movementDirection.Normalize();
        rb.velocity = movementDirection*2;
    }
}
