using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    //setting up public variables
    public Vector2 directionAiming;
    private Rigidbody2D rb;
    public Vector2 movementDirection;
    public bool isDashing;
    //Declare Private variable
    private PlayerController playerController;
    private CombatStats combatStats;
    private PlayerWalkingSoundManager playerWalkingSoundManager;
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
        combatStats = GetComponent<CombatStats>();
        playerWalkingSoundManager = GetComponent<PlayerWalkingSoundManager>();
    }

    void Update()
    {
        playerWalkingSoundManager.isWalking = false;
        if (!isDashing)
        {
            //reset velocity
            rb.velocity = Vector2.zero;

            //pulls controls from unity input manager 
            //Left stick and WASD
            float movementInputHorizontal = playerController.Player.Horizontal.ReadValue<float>();
            float movementInputVertical = playerController.Player.Vertical.ReadValue<float>();
            movementDirection = new Vector2(movementInputHorizontal, movementInputVertical);
            movementDirection.Normalize();
            //Right stick and mouse
            directionAiming = playerController.Player.Mouse.ReadValue<Vector2>();
            //move the player

            if (movementDirection.x > 0.25 || movementDirection.x < -0.25 || movementDirection.y > 0.25 || movementDirection.y < -0.25)
            {
                playerWalkingSoundManager.isWalking = true;
            }

            rb.velocity += movementDirection * combatStats.movementSpeed;
        }
        //If the mouse position is greater than 1 then it is in world space
        //It is impossible for the mouse to be in world space and have a value less than 1 due to it being in "Screenspace" which only allows for integer values
        if (directionAiming.x > 1 || directionAiming.y > 1)
        {
            directionAiming = Camera.main.ScreenToWorldPoint(directionAiming) - transform.position;
            directionAiming.Normalize();
        }
        else
        {
            directionAiming.Normalize();
        }
    }
}
