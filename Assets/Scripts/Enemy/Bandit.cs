using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Enemy
{
    private bool isGrounded;
    private bool isAggressive;
    private int nextMove;

    protected override void Start()
    {
        base.Start();

        abilityName = "소매치기";
        abilityText = "적에게서 얻는 코인 + 1";
        isGrounded = false;
        isAggressive = false;
        nextMove = 1;
        Move();
    }

    protected override void Update()
    {
        base.Update();
        FindPlayer();
    }

    void FixedUpdate()
    {
        if (!isAttacked)
        {
            rb.velocity = new Vector2(nextMove * speed, rb.velocity.y);
        }
    }

    void FindPlayer()
    {
        if (Vector2.Distance(GameManager.instance.playerController.transform.position, transform.position) <= sight && GameManager.instance.playerController.isGrounded)
        {
            speed = 6f;
            isAggressive = true;
            nextMove = (GameManager.instance.playerController.transform.position.x - transform.position.x) > 0 ? 1 : -1;
        }
        else
        {
            isAggressive = false;
            speed = 4f;
        }
    }

    void Move()
    {
        nextMove = nextMove == 1 ? -1 : 1;

        Invoke("Move", 1f);
    }

    public override void GiveStr()
    {
        base.GiveStr();
        GameManager.instance.playerController.coinBoost = 1;
    }
}
