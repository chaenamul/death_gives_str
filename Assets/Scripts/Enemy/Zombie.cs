using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private HitBox zombieattack;
    [SerializeField]
    private float attackRange;

    private Rigidbody2D rb;

    private float timer;
    private int nextMove;
    private bool isAggressive;

    void Awake()
    {
        timer = attackDelay;
        isAggressive = false;
        rb = GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        FindPlayer();
        EnemyAttack();
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
        }
        else
        {
            isAggressive = false;
            rb.velocity = new Vector2(0, rb.velocity.y);
        }
    }

    void EnemyAttack()
    {
        timer += Time.deltaTime;

        if (isAggressive)
        {
            if (Vector2.Distance(target.transform.position, transform.position) > attackRange)
            {
                nextMove = (target.transform.position.x - transform.position.x) > 0 ? 1 : -1;
                rb.velocity = new Vector2(nextMove * speed, rb.velocity.y);
            }
            else if (timer >= attackDelay)
            {
                StartCoroutine(AttackCoroutine());
                timer = 0;
            }
        }
    }

    IEnumerator AttackCoroutine()
    {
        speed = 0f;
        yield return new WaitForSeconds(1.0f);
        zombieattack.transform.position = (target.transform.position - transform.position).normalized * attackRange + transform.position - new Vector3(0.5f, 0f, 0f) * nextMove;
        zombieattack.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        zombieattack.gameObject.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        speed = 3.3f;
        
    }


    void Die()
    {
        gameObject.SetActive(false);
    }
}
