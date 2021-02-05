using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //setting up public variables
    public Vector2 directionAiming;
    private Rigidbody2D rb;
    public float speed = 10.0f;

    //Declare Private variable
    private PlayerController playerController;

    private void Awake()
    {
        playerController = new PlayerController();
    }

    private void OnEnable()
    {
        playerController.Enable();
    }
    private void OnDisable()
    {
        playerController.Disable();
    }
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        //reset velocity
        rb.velocity = Vector2.zero;


        //pulls controls from unity input manager 
        //Left stick and WASD
        float movementInputHorizontal = playerController.Player.Horizontal.ReadValue<float>();
        float movementInputVertical = playerController.Player.Vertical.ReadValue<float>();
        //Right stick and mouse
        directionAiming = playerController.Player.Mouse.ReadValue<Vector2>();

        //Left mouse button and left most face button
        float Attack = playerController.Player.Attack.ReadValue<float>();

        //move the player
        rb.velocity += new Vector2(movementInputHorizontal, movementInputVertical) * speed;

        //If the mouse position is greater than 1 then it is in world space
        //It is impossible for the mouse to be in world space and have a value less than 1 due to it being in "Screenspace" which only allows for integer values
        if (directionAiming.x > 1 || directionAiming.y > 1)
        {
            directionAiming = (Camera.main.ScreenToWorldPoint(directionAiming) - transform.position).normalized;
        }
Debug.Log(directionAiming);
        //if the attack button is pressed
        if (Attack>0.5)
        {
            //insert attack code here
            

        }
    }
}
