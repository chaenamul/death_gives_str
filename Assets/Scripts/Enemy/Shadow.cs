using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shadow : Enemy
{
    public Rigidbody2D cloneRb;

    private bool isAggressive = false;

    void Update()
    {
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
        if (Vector2.Distance(target.transform.position, transform.position) <= sight)
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
        cloneRb.velocity += ((Vector2)(target.transform.position - cloneRb.transform.position)).normalized * 2 * Time.deltaTime;
        if (cloneRb.velocity.magnitude > 8)
        {
            cloneRb.velocity = cloneRb.velocity.normalized * 8;
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
