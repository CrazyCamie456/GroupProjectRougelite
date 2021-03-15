using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class SlimeAttack : MonoBehaviour
{
    Animator anim;
    private ChargedAttacks charger;
    private AIPath rb;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<AIPath>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        charger = gameObject.GetComponentInParent<ChargedAttacks>();

    }

    // Update is called once per frame
    void Update()
    {
        if (charger.isCharging)
            anim.SetTrigger("Attack");
        else anim.SetTrigger("Idle");

        if (rb.velocity.x > 0)
            sprite.flipX = true;

        if (rb.velocity.x < 0)
            sprite.flipX = false;
    }
}
