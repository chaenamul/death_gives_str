using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{
    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private HitBox zombieAttack;
    [SerializeField]
    private float attackRange;

    private float timer;
    private bool isAggressive;

    void Awake()
    {
        timer = attackDelay;
        isAggressive = false;
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
            if(hp<=0)
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
        }
    }
    
    void EnemyAttack()
    {
        timer += Time.deltaTime;

        if(isAggressive)
        {
            if(Vector2.Distance(target.transform.position,transform.position) > attackRange)
            {
                transform.position += (target.transform.position - transform.position).normalized * speed * Time.deltaTime;
            }
            else if (timer >= attackDelay)
            {
                StartCoroutine(SwordAttackCoroutine());
                timer = 0f;
            }
        }

    }

    IEnumerator SwordAttackCoroutine()
    {
        yield return new WaitForSeconds(1.0f);
        zombieAttack.transform.position = (target.transform.position - transform.position).normalized * attackRange + transform.position;
        zombieAttack.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        zombieAttack.gameObject.SetActive(false);
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}   