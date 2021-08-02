using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : MonoBehaviour
{
    public int hp;
    public int dmg;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float sight;
    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private HitBox Zombieattack;
    [SerializeField]
    private float attackRange;

    [SerializeField]
    private GameObject target;

    private float timer;
    private bool isAggressive;

    void Start()
    {
        timer = attackDelay;
        isAggressive = false;
    }

    void Update()
    {
        FindPlayer();
        MonsterAttack();
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hp -= GameManager.instance.playerController.dmg;
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
    
    void MonsterAttack()
    {
        timer += Time.deltaTime;

        if(isAggressive)
        {
            if(Vector2.Distance(target.transform.position,transform.position)>attackRange)
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
        Zombieattack.transform.position = (target.transform.position - transform.position).normalized * attackRange + transform.position;
        Zombieattack.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        Zombieattack.gameObject.SetActive(false);
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}   