using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Member : Enemy
{
    private int nextMove;

    protected override void Start()
    {
        base.Start();
        abilityName = "광기";
        abilityText = "몬스터에게 가하는 데미지 +5";
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
        GameManager.instance.Dmg+=5;
    }
}
