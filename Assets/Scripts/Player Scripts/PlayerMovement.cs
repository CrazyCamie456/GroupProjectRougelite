using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //setting up public variables
    public Vector2 directionAiming;
    private Rigidbody2D rb;
    public float speed = 10.0f;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {

        directionAiming = (Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position).normalized;
        rb.velocity = Vector2.zero;
        //pulls controls from unity input manager 
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");
        rb.velocity += new Vector2(horizontal, vertical) * speed;



        if (Input.GetButton("Attack"))
        {
            //insert attack code here

        }
    }
}
