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

    private float timer;
    private int nextMove;
    private bool isAggressive;

    protected override void Start()
    {
        base.Start();

        zombieattack.dmg = dmg;
        zombieattack.subject = this;

        abilityName = "한도 증가";
        timer = attackDelay;
        isAggressive = false;
    }

    protected override void Update()
    {
        base.Update();
        FindPlayer();
        EnemyAttack();
    }

    void FindPlayer()
    {
        if (Vector2.Distance(GameManager.instance.playerController.transform.position, transform.position) <= sight)
        {
            isAggressive = true;
        }
        else
        {
            isAggressive = false;
            if (!isAttacked)
            {
                rb.velocity = new Vector2(0, rb.velocity.y);
            }
        }
    }

    void EnemyAttack()
    {
        timer += Time.deltaTime;

        if (isAggressive)
        {
            if (Vector2.Distance(GameManager.instance.playerController.transform.position, transform.position) > attackRange)
            {
                nextMove = (GameManager.instance.playerController.transform.position.x - transform.position.x) > 0 ? 1 : -1;
                if (!isAttacked)
                {
                    rb.velocity = new Vector2(nextMove * speed, rb.velocity.y);
                }
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
        zombieattack.transform.position = (GameManager.instance.playerController.transform.position - transform.position).normalized * attackRange + transform.position - new Vector3(0.5f, 0f, 0f) * nextMove;
        zombieattack.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        zombieattack.gameObject.SetActive(false);
        yield return new WaitForSeconds(3.0f);
        speed = 3.3f;
        
    }

    public override void GiveStr()
    {
        base.GiveStr();
        GameManager.instance.maxHp += 20;
    }
}
