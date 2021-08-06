using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHitBox : HitBox
{
    public HitBox explosion;
    private void Awake()
    {
        explosion.subject = subject;

    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
        if (subject as Enemy && !collision.gameObject.tag.Contains("Enemy") || subject as PlayerController && !(collision.gameObject.tag == "Player"))
        {
            gameObject.SetActive(false);
            CoroutineManager.instance.Coroutine(Explosion(transform.position));
        }
    }
    IEnumerator Explosion(Vector3 pos)
    {
        explosion.gameObject.transform.position = pos;
        explosion.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.3f);
        explosion.gameObject.SetActive(false);
        
    }
}