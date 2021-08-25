using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : Enemy
{
    public Rigidbody2D cloneRb;

    private bool isAggressive = false;

    protected override void Start()
    {
        base.Start();

        abilityName = "도플갱어";
    }
    protected override void Update()
    {
        base.Update();
        if (!isAggressive)
        {
            FindPlayer();
        }
    }

    void FixedUpdate()
    {
        if (isAggressive)
        {
            Move();
        }
    }

    void FindPlayer()
    {
        if (Vector2.Distance(GameManager.instance.playerController.transform.position, transform.position) <= sight)
        {
            isAggressive = true;
            cloneRb.gameObject.SetActive(true);
        }
        else
        {
            cloneRb.gameObject.SetActive(false);
        }
    }

    void Move()
    {
        cloneRb.velocity += ((Vector2)(GameManager.instance.playerController.transform.position - cloneRb.transform.position)).normalized;
        if (cloneRb.velocity.magnitude > 6)
        {
            cloneRb.velocity = cloneRb.velocity.normalized * 6;
        }
    }
    public override void GiveStr()
    {
        base.GiveStr();
        ///나중에 구현
    }
}
