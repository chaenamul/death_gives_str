using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Enemy
{
    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private HitBox skeletonSword;
    [SerializeField]
    private float attackRange;

    private Rigidbody2D rb;

    private float timer;
    private int nextMove;
    private bool isAggressive;

    void Awake()
    {
        abilityName = "ฟ๘วั";
        timer = attackDelay;
        isAggressive = false;
        rb = GetComponent<Rigidbody2D>();

        skeletonSword.dmg = dmg;
        skeletonSword.subject = this;

        Move();
    }

    void Update()
    {
        FindPlayer();
        EnemyAttack();
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
        if (Vector2.Distance(target.transform.position, transform.position) <= sight)
        {
            isAggressive = true;
            speed = 4f;
        }
        else
        {
            isAggressive = false;
            speed = 2f;
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
            }
            else if (timer >= attackDelay)
            {
                StartCoroutine(SwordAttackCoroutine());
                timer = 0f;
            }
        }
    }

    public override void GiveStr()
    {
        base.GiveStr();
        GameManager.instance.dmg += 5;
    }

    IEnumerator SwordAttackCoroutine()
    {
        yield return new WaitForSeconds(0.5f);
        skeletonSword.transform.position = (target.transform.position - transform.position).normalized * attackRange + transform.position;
        skeletonSword.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        skeletonSword.gameObject.SetActive(false);
    }
}