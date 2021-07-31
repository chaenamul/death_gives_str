using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : Enemy
{
    [SerializeField]
    private float knifeRange;
    [SerializeField]
    private float daggerRange;
    [SerializeField]
    private HitBox hitbox;

    private Rigidbody2D rb;

    private int nextMove;

    void Awake()
    {

    }

    void Update()
    {
        FindPlayer();
    }

    void FixedUpdate()
    {
        
    }

    void FindPlayer()
    {
        var dist = Vector2.Distance(target.transform.position, transform.position);
        
        if ( dist <= knifeRange)
        {

        }
        else if (knifeRange < dist && dist <= daggerRange)
        {
            nextMove = 0;
            DaggerAttack();
        }
        else
        {
            nextMove = 0;
        }
    }

    void DaggerAttack()
    {

    }
}