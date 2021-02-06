using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dash : MonoBehaviour
{

    [HideInInspector] public bool invincibility;
    public float dashSpeed;
    public int dashLimit;

    private PlayerController playerController;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    [SerializeField]
    private float dashDelay;
    [SerializeField]
    private float dashDuration;

    private void Awake()
    {
        playerController = new PlayerController();
        playerMovement = GetComponent<PlayerMovement>();
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
        //Left shift, space and the right face button
        float dashInput = playerController.Player.Dash.ReadValue<float>();

        if (dashDelay > 0)
        {
            dashDelay -= Time.deltaTime;
        }

        if (dashDelay < 0 && dashLimit < 2)
        {
            dashDelay = 2.0f;
            dashLimit++;
        }

        //check if the player has a dash avaliable, is currently not dashing and is trying to dash
        if (dashLimit > 0 && dashDuration < 0 && dashInput > 0)
        {
            //while the player dashes disable input
            playerMovement.isDashing = true;
            rb.velocity = playerMovement.movementDirection * dashSpeed;
            dashDelay = 2.0f;
            dashDuration = 0.1f;
            dashLimit--;
        }

        //after the player has dashed resume movement
        if (dashDuration > 0)
        {
            dashDuration -= Time.deltaTime;
        }
        else
        {
            playerMovement.isDashing = false;
        }


    }
}
