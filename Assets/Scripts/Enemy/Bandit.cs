using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bandit : Enemy
{
    [SerializeField]
    private float jumpForce;

    private Rigidbody2D rb;

    private bool isGrounded;
    private int nextMove;

    void Awake()
    {
        isGrounded = false;
        nextMove = 1;
        rb = GetComponent<Rigidbody2D>();

        Invoke("Jump", Random.Range(1f, 2.5f));
        Move();
    }
    void Start()
    {
        
    }

    void Update()
    {
        FindPlayer();
    }

    void FixedUpdate()
    {
        rb.velocity = new Vector2(nextMove, rb.velocity.y);
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
        if (Vector2.Distance(target.transform.position, transform.position) <= sight && GameManager.instance.playerController.isGrounded)
        {
            speed = 12.5f;
            transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
        }
        else
        {
            speed = 4f;
        }
    }

    void Jump()
    {
        if (isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }

        Invoke("Jump", Random.Range(1f, 2.5f));
    }

    void Move()
    {
        nextMove = nextMove == 1 ? -1 : 1;

        Invoke("Move", 5f);
    }
}
