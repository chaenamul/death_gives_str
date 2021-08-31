using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttackHitBox : HitBox
{
    public bool isDisposable { get; set; } = false;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (subject as PlayerController && collision.tag != "Player" || subject as Enemy && collision.tag != "Enemy")
        {
            if (!isDisposable)
                gameObject.SetActive(false);
            else
                Destroy(gameObject);
        }
    }
}
