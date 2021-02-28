using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAgroRange : MonoBehaviour
{

    public float agroRange;
    public Sprite activeSprite;
    public Sprite sleepSprite;
    private Rigidbody2D rb;
    private Rigidbody2D rbPlayer;
    private CombatStats combatStats;
    private SpriteRenderer sp;
    private Vector2 distanceFromPlayer;

    void Start()
    {
        combatStats = GetComponent<CombatStats>();
        rb = GetComponent<Rigidbody2D>();
        //Get the player rigidbody
        rbPlayer = GameObject.Find("Player").GetComponent<Rigidbody2D>();
        sp = GetComponentInChildren<SpriteRenderer>();
    }

    void Update()
    {
        distanceFromPlayer = rbPlayer.position - rb.position;

        if (Vector3.Magnitude(distanceFromPlayer) < agroRange)
        {
            distanceFromPlayer.Normalize();
            rb.velocity = Vector2.zero;
            if (!combatStats.isCrowdControlled)
                rb.velocity = distanceFromPlayer * combatStats.movementSpeed;
            sp.sprite = activeSprite;
        }
        else
        {
            sp.sprite = sleepSprite;
        }


    }
}
