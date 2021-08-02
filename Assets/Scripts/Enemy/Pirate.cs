using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Pirate : Enemy
{
    // Start is called before the first frame update
    Vector3 initLoc;
    public int moveVector; // -1 이면 초기에 왼쪽으로 움직임
    Rigidbody2D rbody;
    private bool found;
    private float delay;
    public HitBox hitbox;
    Attack attack;
    void Start()
    {
        delay = 0.0f;
        found = false;
        rbody = GetComponent<Rigidbody2D>();
        initLoc = transform.position;
        hp = 70;
        dmg = 30;
        speed = GameManager.instance.playerController.speed * 2f / 3f;
        sight = 25f;
        attack = new ParabolaAttack(dmg, 6.0f, hitbox, gameObject, 0, true);
    }

    // Update is called once per frame
    void Update()
    {
        delay = delay - Time.deltaTime < 0 ? 0 : delay - Time.deltaTime;
        FindPlayer();
        Move();
        attack.DelayUpdate();
        if (found)
        {
            if (delay == 0)
            {
                ((ParabolaAttack)attack).xSpeed = (GameManager.instance.playerController.transform.position.x - transform.position.x) / 3.0f ;
                attack.Execute(AttackType.normal);
                delay = 6.0f;
            }
        }
        
    }

    void Move()
    {
        if (found)
        {
            if (delay <= 3f && Mathf.Abs(transform.position.x - GameManager.instance.playerController.transform.position.x) > 0.1f)
            {
                transform.position += new Vector3(GameManager.instance.playerController.transform.position.x - transform.position.x, 0, 0).normalized * speed * Time.deltaTime;
            }
        }
        else
        {
            if (transform.position.x - initLoc.x < -3)
                moveVector = 1;
            else if (transform.position.x - initLoc.x > 3)
                moveVector = -1;
            transform.position += new Vector3(moveVector * speed * Time.deltaTime, 0, 0);
        }
        
    } 

    void FindPlayer()
    {
        if (Vector3.Distance(GameManager.instance.playerController.transform.position, transform.position) <= sight)
            found = true;
        else
            found = false;
    }
}
