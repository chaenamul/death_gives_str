using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class FireBall : HitBox
{
    private void Awake()
    {
        HitBox parent = transform.parent.GetComponent<HitBox>();
        dmg = parent.dmg;
        subject = parent.subject;
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if(collision.tag == "Player")
        {
            GameManager.instance.playerController.enabled = false;
            CoroutineManager.instance.Coroutine(EndStun());
        }
        if (!collision.tag.Contains("Enemy") && !(collision.tag == this.tag))
        {
            Destroy(gameObject);
        }
    }
    private IEnumerator EndStun()
    {
        yield return new WaitForSeconds(Dragon.stunTime);
        GameManager.instance.playerController.enabled = true;
    }
}
