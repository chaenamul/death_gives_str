using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Sniper : Enemy
{
    [SerializeField]
    private float delay;
    private Attack sniping;
    private Rigidbody2D rb;
    void Start() 
    {
        rb = GetComponent<Rigidbody2D>();
        delay = 5.0f;
        hp = 100;
        dmg = 50;
        speed = 0;
        target = GameManager.instance.playerController.gameObject;
        sniping = new HitScanRangeAttack(dmg, delay, gameObject, 3.0f);
    }

    void Update()
    {
        sniping.DelayUpdate();
        if (InScreen())
        {
            sniping.Execute(AttackType.normal);
        }
    }

    bool InScreen()
    {
        float whRatio = Screen.width / (float)Screen.height;
        float sizeHRatio = Camera.main.orthographicSize / (float)Screen.height;
        return (math.abs(transform.position.x - Camera.main.transform.position.x) <= Screen.height * whRatio * sizeHRatio) & (math.abs(transform.position.y - Camera.main.transform.position.y) <= Screen.height * sizeHRatio);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            hp -= GameManager.instance.dmg;
            if (hp <= 0)
            {
                Die();
            }
        }
    }

    void Die()
    {
        gameObject.SetActive(false);
    }
}
