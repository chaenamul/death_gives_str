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
    public HitBox bomb;
    public HitBox sword;
    Attack attack;
    private float stiffAfterAttack;
    private float delayMax;
    bool isGhost = false;
    void Start()
    {
        delayMax = 6.0f;
        stiffAfterAttack = 3.0f;
        delay = 0.0f;
        found = false;
        rbody = GetComponent<Rigidbody2D>();
        initLoc = transform.position;
        hp = 70;
        dmg = 30;
        speed = GameManager.instance.playerController.speed * 2f / 3f;
        sight = 25f;
        attack = new ParabolaAttack(dmg, 6.0f, bomb, gameObject, 0, true);
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
                if (!isGhost)
                {
                    ((ParabolaAttack)attack).xSpeed = (GameManager.instance.playerController.transform.position.x - transform.position.x) / 3.0f;
                    attack.Execute(AttackType.normal);
                    delay = delayMax;
                }
                else if(sword.transform.localScale.x>Mathf.Abs(transform.position.x - GameManager.instance.playerController.transform.position.x))
                {                     
                    attack.Execute(AttackType.normal);
                    delay = delayMax;
                }
                    
                
            }
        }
        
    }

    void Move()
    {
        if (found)
        {
            if (delayMax - delay >= stiffAfterAttack && Mathf.Abs(transform.position.x - GameManager.instance.playerController.transform.position.x) > 1.5f)
            {
                Debug.Log(Mathf.Abs(transform.position.x - GameManager.instance.playerController.transform.position.x));
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

    void Die()
    {
        if (isGhost)
            gameObject.SetActive(false);
        else
        {
            isGhost = true;
            hp = 10;
            sight = 15;
            dmg = 10;
            stiffAfterAttack = 2.0f;
            delayMax = 2.0f;
            delay = 0.0f;
            attack = new SwordAttack(dmg, delayMax, sword, gameObject);
        }
    }

    void GetDmg(int dmg)
    {
        hp = hp - dmg;
        if (hp <= 0)
            Die();

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "PlayerHitBox")
        {
            GetDmg(GameManager.instance.playerController.dmg);
        }
    }


}
