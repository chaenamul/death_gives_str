using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : Monster
{
    // Mob Settings
    [SerializeField]
    private int hp;
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

    private Attack attack;
    private bool isAggressive;

    void Start()
    {
        attack = new SwordAttack(dmg, attackDelay, skeletonSword, gameObject);
        isAggressive = false;
    }

    void Update()
    {
        FindPlayer();
        MonsterAttack();
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
        }
    }

    void MonsterAttack()
    {
        attack.DelayUpdate();
        if (isAggressive)
        {
            //Debug.Log("Aggressive");
            if (Vector3.Distance(GameManager.instance.playerController.transform.position, transform.position) > attackRange)
            {
                transform.position += (GameManager.instance.playerController.transform.position - transform.position).normalized * speed * Time.deltaTime;
            }
            else
            {
                attack.Execute(AttackType.normal);
            }
        }
    }
}