using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Pathfinding;

public class AggroWalk : MonoBehaviour
{
    Animator anim;
    private EnemyRangedAiPathHelper pathHelper;
    private AIPath rb;
    private SpriteRenderer sprite;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponentInParent<AIPath>();
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        pathHelper = gameObject.GetComponentInParent<EnemyRangedAiPathHelper>();

    }

    // Update is called once per frame
    void Update()
    {
        if (!pathHelper.canFire)
            anim.SetTrigger("AggroWalk");
        else anim.SetTrigger("PlayerDead");

        if (rb.velocity.x < 0)
            sprite.flipX = true;

        if (rb.velocity.x > 0)
            sprite.flipX = false;
    }
}
