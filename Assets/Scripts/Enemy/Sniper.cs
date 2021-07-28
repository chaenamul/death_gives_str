using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.EventSystems;

public class Sniper : Enemy
{
    // Start is called before the first frame update
    [SerializeField]
    private HitBox bullet;
    private float delay;
    private Attack sniping;
    private Rigidbody2D rigidbody;
    void Start() 
    {
        rigidbody = GetComponent<Rigidbody2D>();
        delay = 5.0f;
        hp = 100;
        dmg = 50;
        bullet.dmg = 50;
        speed = 0;
        target = GameManager.instance.playerController.gameObject;
        sniping = new TargetingRangeAttack(dmg, delay, bullet, gameObject, 30.0f);
    }

    // Update is called once per frame
    void Update()
    {
        sniping.DelayUpdate();
        if (InScreen())
        {
            sniping.Execute(AttackType.normal);
        }
    }

    private bool InScreen()
    {
        float whRatio = Screen.width / (float)Screen.height;
        float sizeHRatio = Camera.main.orthographicSize / (float)Screen.height;
        return (math.abs(transform.position.x - Camera.main.transform.position.x) <= Screen.height * whRatio * sizeHRatio) & (math.abs(transform.position.y - Camera.main.transform.position.y) <= Screen.height * sizeHRatio);
    }
    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag.Contains("Player"))
        {
            hp -= GameManager.instance.playerController.dmg;
            if (hp <= 0)
            {
                Die();
            }
        }
    }
    private void Die()
    {
        gameObject.SetActive(false);
    }
}
