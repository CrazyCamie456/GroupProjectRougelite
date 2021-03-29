using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
public class Dash : MonoBehaviour
{

    [HideInInspector] public bool invincibility;
    public float dashSpeed;
    public int maxDashLimit = 2;
    public float maxDashDelay = 2.0f;
    public float maxDashDuration = 0.1f;

    private int dashLimit;
    private PlayerController playerController;
    private PlayerMovement playerMovement;
    private Rigidbody2D rb;
    private float dashDuration;
    private float dashDelay;

    private PlayerCombatStats combatStats;
    bool notHoldingDash = true;
    bool dashOverPit = false;
    Tilemap[] tile;
    private Vector2 dashStartPos;
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
        combatStats = GetComponent<PlayerCombatStats>();

        rb = GetComponent<Rigidbody2D>();
        dashDuration = maxDashDuration;
        dashDelay = maxDashDelay;
        dashLimit = maxDashLimit;
    }

    void Update()
    {
        //Left shift, space and the right face button
        float dashInput = playerController.Player.Dash.ReadValue<float>();
        tile = FindObjectsOfType<Tilemap>();
        if (dashDelay >= 0.0f)
        {
            dashDelay -= Time.deltaTime;
        }

        if (dashDelay < 0.0f && dashLimit < maxDashLimit)
        {
            dashDelay = maxDashDelay;
            dashLimit++;
        }

        //check if the player has a dash avaliable, is currently not dashing and is trying to dash and is not holding the dash button down from the previous dash
        if (dashLimit > 0 && dashDuration < 0.0f && dashInput > 0.0f && notHoldingDash)
        {
            dashStartPos = transform.position;

            dashOverPit = true;

            gameObject.layer = 12;

            //while the player dashes disable input
            playerMovement.isDashing = true;
            rb.velocity = playerMovement.movementDirection * dashSpeed;
            dashDelay = maxDashDelay;
            dashDuration = maxDashDuration;
            dashLimit--;
            notHoldingDash = false;
            combatStats.SetIFrames(dashDuration);
        }

        //check if the player has released the dash button
        if (dashInput < 1.0f)
        {
            notHoldingDash = true;
        }

        //if (playerMovement.isDashing && dashDuration <= 0.0f)
        //{
        //    Vector3Int pos = new Vector3Int((int)transform.position.x, (int)transform.position.y, (int)transform.position.z);
        //    foreach (Tilemap t in tile)
        //    {
        //        var name = t.
        //        name.get
        //        if (name == null) { }
        //        //else if (name == "Pit")
        //        //{
        //        //    transform.position = dashStartPos;
        //        //}
        //    //    Debug.Log("Success!!!maybe?");
        //    }

        //}

        //after the player has dashed resume movement
        if (dashDuration > 0.0f)
        {
            dashDuration -= Time.deltaTime;
        }
        else
        {
            gameObject.layer = 0;
            dashOverPit = false;
            playerMovement.isDashing = false;
        }

    }
}
