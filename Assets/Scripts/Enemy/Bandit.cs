using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Enemy
{
    [SerializeField]
    private float jumpForce;

    private Rigidbody2D rb;

    private bool isGrounded;
    private bool isAggressive;
    private int nextMove;

    void Awake()
    {
        isGrounded = false;
        isAggressive = false;
        nextMove = 1;
        rb = GetComponent<Rigidbody2D>();

        Invoke("Jump", Random.Range(1f, 2.5f));
        Move();
    }

    void Update()
    {
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

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hp -= GameManager.instance.dmg;
            if (hp <= 0)
            {
                Die();
            }
        }
    }

    void FindPlayer()
    {
        if (Vector2.Distance(target.transform.position, transform.position) <= sight && GameManager.instance.playerController.isGrounded)
        {
            speed = 6f;
            isAggressive = true;
            nextMove = (target.transform.position.x - transform.position.x) > 0 ? 1 : -1;
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

    void Die()
    {
        gameObject.SetActive(false);
    }
}
