using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Enemy
{
    [SerializeField]
    private float jumpForce;

    private bool isGrounded;
    private bool isAggressive;
    private int nextMove;

    protected override void Start()
    {
        base.Start();

        abilityName = "소매치기";
        isGrounded = false;
        isAggressive = false;
        nextMove = 1;

        Invoke("Jump", Random.Range(1f, 2.5f));
        Move();
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
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

    void Jump()
    {
        if (isGrounded && isAggressive)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        Invoke("Jump", Random.Range(1f, 2.5f));
    }

    void Move()
    {
        nextMove = nextMove == 1 ? -1 : 1;

        Invoke("Move", 1f);
    }

    public override void GiveStr()
    {
        base.GiveStr();
        ///상자 구현 후 구현 필요
    }
}
