using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Member : Enemy
{
    private Rigidbody2D rb;

    private int nextMove;

    protected override void Start()
    {
        base.Start();
        rb = GetComponent<Rigidbody2D>();
    }

    protected override void Update()
    {
        base.Update();
        FindPlayer();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(nextMove * speed, rb.velocity.y);
    }

    void FindPlayer()
    {
        if (Vector3.Distance(GameManager.instance.playerController.transform.position, transform.position) <= sight)
        {
            nextMove = (GameManager.instance.playerController.transform.position.x - transform.position.x) > 0 ? 1 : -1;
        }
        else
        {
            nextMove = 0;
        }
    }

    public override void GiveStr()
    {
        base.GiveStr();
        GameManager.instance.playerController.isDmgBoosted = true;
        GameManager.instance.Dmg++;
    }
}
