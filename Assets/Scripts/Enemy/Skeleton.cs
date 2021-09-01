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

    private float timer;
    private int nextMove;
    private bool isAggressive;

    protected override void Start()
    {
        base.Start();

        abilityName = "원한";
        abilityText = "공격력 5 증가";
        timer = attackDelay;
        isAggressive = false;

        skeletonSword.dmg = dmg;
        skeletonSword.subject = this;

        Move();
        anim.SetBool("isWalking", true);
    }

    protected override void Update()
    {
        base.Update();
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
        if (Vector2.Distance(GameManager.instance.playerController.transform.position, transform.position) <= sight)
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
            if (Vector2.Distance(GameManager.instance.playerController.transform.position, transform.position) > attackRange)
            {
                nextMove = (GameManager.instance.playerController.transform.position.x - transform.position.x) > 0 ? 1 : -1;
            }
            else if (timer >= attackDelay)
            {
                StartCoroutine(SwordAttackCoroutine());
                timer = 0f;
            }
        }
    }

    public override void GetDmg(int dmg)
    {
        base.GetDmg(dmg);
    }

    public override void GiveStr()
    {
        base.GiveStr();
        GameManager.instance.Dmg += 5;
    }

    IEnumerator SwordAttackCoroutine()
    {
        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.5f);
        skeletonSword.transform.position = (GameManager.instance.playerController.transform.position - transform.position).normalized * attackRange + transform.position;
        skeletonSword.gameObject.SetActive(true);
        SoundManager.Instance.PlaySFX("skeleton_attack", 0.3f);
        yield return new WaitForSeconds(0.1f);
        anim.SetBool("isAttacking", false);
        skeletonSword.gameObject.SetActive(false);
    }
}