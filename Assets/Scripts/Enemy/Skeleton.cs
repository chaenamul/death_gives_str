using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    // Mob Settings
    public int hp;
    public int dmg;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float sight;
    [SerializeField]
    private float attackDelay;
    [SerializeField]
    private HitBox skeletonSword;
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
            if (hp <= 0)
            {
                Die();
            }
        }
    }

    void FindPlayer()
    {
        if (Vector3.Distance(GameManager.instance.playerController.transform.position, transform.position) <= sight)
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

    void MonsterAttack()
    {
        timer += Time.deltaTime;

        if (isAggressive)
        {
            if (Vector3.Distance(target.transform.position, transform.position) > attackRange)
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
        yield return new WaitForSeconds(0.5f);
        skeletonSword.transform.position = (target.transform.position - transform.position).normalized * attackRange + transform.position;
        skeletonSword.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        skeletonSword.gameObject.SetActive(false);
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}