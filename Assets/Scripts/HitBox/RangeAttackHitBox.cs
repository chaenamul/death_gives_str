using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackHitBox : HitBox
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.tag.Contains("Enemy"))
        {
            gameObject.SetActive(false);
            if(collision.gameObject.tag == "Player")
            {
                GameManager.instance.hp -= dmg;
            }
        }

    }
}
