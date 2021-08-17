using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : Enemy
{
    [SerializeField]
    private GameObject memberPrefab;

    private Rigidbody2D rb;

    private float stopTimer = 2f;
    private float spawnTimer = 7f;
    private int nextMove;

    protected override void Start()
    {
        base.Start();

        abilityName = "Á¶Á¾";
        Move();
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

    void Move()
    {
        nextMove = nextMove == 1 ? -1 : 1;

        Invoke("Move", 1.5f);
    }

    void FindPlayer()
    {
        if (Vector3.Distance(GameManager.instance.playerController.transform.position, transform.position) <= sight)
        {
            stopTimer -= Time.deltaTime;
            if (stopTimer >= 0)
            {
                speed = 0f;
            }
            else
            {
                speed = 4f;
            }

            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                Spawn();
                spawnTimer = 7f;
            }
        }
        else
        {
            stopTimer = 2f;
            speed = 4f;
        }
    }

    void Spawn()
    {
        for (int i = -3; i <= 3; i++)
        {
            var member = Instantiate(memberPrefab);
            member.transform.position = GameManager.instance.playerController.transform.position + new Vector3(i * 3, 3f, 0);
        }
    }

    public override void GiveStr()
    {
        base.GiveStr();
        GameManager.instance.playerController.gainedInvincible = true;
    }
}
