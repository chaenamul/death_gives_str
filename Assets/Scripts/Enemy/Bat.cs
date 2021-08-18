using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bat : Enemy
{
    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private HitBox batattack;
    [SerializeField]
    private float attackRange;

    private Rigidbody2D rb;

    private float timer;
    private int nextMovex;
    private int nextMovey;
    private bool isAggressive;

    private Vector2 Movement1;
    private Vector2 Movement2;
    private bool Movecheck=true;

    private float wing;
    private bool wingbool=true;
    

    protected override void Start()
    {
        base.Start();

        batattack.dmg = dmg;
        batattack.subject = this;

        timer = attackDelay;
        isAggressive = false;
        rb = GetComponent<Rigidbody2D>();
        InvokeRepeating("WingMovement", 0f, 0.5f);
    }

    protected override void Update()
    {
        base.Update();
        if(Movecheck)
        {
            Movement1 = GameManager.instance.playerController.transform.position;
            Movecheck = false;
        }
        else
        {
            Movement2 = GameManager.instance.playerController.transform.position;
            Movecheck = true;
        }
        FindPlayer();
        EnemyAttack();
    }

    void FindPlayer()
    {
        if (Movement1!=Movement2)
        { 
            isAggressive = true;
            speed = 4.5f;
            rb.gravityScale = 0f;
        }
        else
        {
            isAggressive = false;
            speed = 0f;
            rb.gravityScale = -1f;
        }

    }

    void EnemyAttack()
    {
        timer += Time.deltaTime;
        if(isAggressive)
        {
            if(Vector2.Distance(GameManager.instance.playerController.transform.position, transform.position) > attackRange)
            {
                nextMovex = (GameManager.instance.playerController.transform.position.x - transform.position.x) > 0 ? 1 : -1;
                nextMovey = ((GameManager.instance.playerController.transform.position.y+1f) - transform.position.y) > 0 ? 1 : -1;
                rb.velocity = new Vector2(nextMovex * speed, nextMovey * speed * wing);

            }
            else if (timer >= attackDelay)
            {
                StartCoroutine(batAttackCoroutine());
                timer = 0f;    
            }
        }
    }
    
    IEnumerator batAttackCoroutine()
    {
        yield return new WaitForSeconds(0.2f);
        batattack.transform.position = (GameManager.instance.playerController.transform.position - transform.position).normalized * attackRange + transform.position;
        batattack.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        batattack.gameObject.SetActive(false);
    }
    void WingMovement()
    {
        if(wingbool)
        {
            wing = 1f;
            wingbool = false;
        }
        else
        {
            wing = 0.5f;
            wingbool = true;
        }

    }

    
}
