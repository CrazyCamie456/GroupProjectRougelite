using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WalkBehaviourTriggers : MonoBehaviour
{
    Animator anim;
    private EnemyRangedAiPathHelper pathHelper;
    private Rigidbody2D rb;
    private SpriteRenderer sprite;
    private PlayerMovement movement;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<Rigidbody2D>();
        movement = GetComponentInParent<PlayerMovement>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        pathHelper = gameObject.GetComponentInParent<EnemyRangedAiPathHelper>();
    }

    // Update is called once per frame
    void Update()
    {

        sprite.flipX = movement.directionAiming.x < 0;

        if (rb.velocity.x != 0 || rb.velocity.y != 0)
            anim.SetBool("IsMoving", true);

        else anim.SetBool("IsMoving", false);
    }
}
