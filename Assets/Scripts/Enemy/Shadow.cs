using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : Enemy
{
    public Rigidbody2D cloneRb;

    private bool isAggressive = false;
    [SerializeField]
    private PlayerShadow playerShadow;

    protected override void Start()
    {
        base.Start();

        abilityName = "도플갱어";
        abilityText = "플레이어의 행동을 따라하는 그림자 생성";
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
        if (!GameManager.instance.abilities.Contains(abilityName))
        {
            Instantiate(playerShadow, GameManager.instance.playerController.transform);
        }
        base.GiveStr();
        
        
    }
}
