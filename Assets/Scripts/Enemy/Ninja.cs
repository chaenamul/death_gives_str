using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ninja : Enemy
{
    [SerializeField]
    private float knifeRange;
    [SerializeField]
    private float daggerRange;
    [SerializeField]
    private float attackRange;
    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private HitBox hitbox;

    private Rigidbody2D rb;

    private float knifeTimer;
    private float knifeAttackTimer;
    private float daggerTimer;
    private int nextMove;

    protected override void Start()
    {
        base.Start();

        abilityName = "´ë½¬";
        knifeTimer = 2f;
        knifeAttackTimer = 1f;
        daggerTimer = 0f;
        nextMove = 0;
        rb = GetComponent<Rigidbody2D>();

        hitbox.dmg = dmg;
        hitbox.subject = this;
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

    void FindPlayer()
    {
        var dist = Vector2.Distance(target.transform.position, transform.position);

        if (dist <= knifeRange)
        {
            knifeTimer += Time.deltaTime;

            if (knifeTimer < 2f)
            {
                nextMove = 0;
            }
            else
            {
                nextMove = (target.transform.position.x - transform.position.x) > 0 ? 1 : -1;
                KnifeAttack();
            }
        }
        else if (knifeRange < dist && dist <= daggerRange)
        {
            nextMove = 0;
            DaggerAttack();
        }
        else
        {
            nextMove = 0;
        }
    }

    void KnifeAttack()
    {
        knifeAttackTimer += Time.deltaTime;

        if (knifeAttackTimer >= attackDelay && Vector2.Distance(target.transform.position, transform.position) <= attackRange)
        {
            StartCoroutine(KnifeAttackCoroutine());
            knifeTimer = 0f;
            knifeAttackTimer = 0f;
        }

    }

    IEnumerator KnifeAttackCoroutine()
    {
        hitbox.transform.position = (target.transform.position - transform.position).normalized * attackRange + transform.position;
        hitbox.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hitbox.gameObject.SetActive(false);
    }

    void DaggerAttack()
    {
        hitbox.gameObject.SetActive(true);
        hitbox.GetComponent<Rigidbody2D>().AddForce((target.transform.position - transform.position).normalized * 4 * Time.deltaTime, ForceMode2D.Impulse);
    }
    public override void GiveStr()
    {
        base.GiveStr();
        GameManager.instance.playerController.canDash = true;
    }
}