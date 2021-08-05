using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackHitBox : HitBox
{

    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (subject as PlayerController && collision.tag != "Player" || subject as Enemy && collision.tag != "Enemy")
        {
            gameObject.SetActive(false);
        }
    }
}
