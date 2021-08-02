using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplosionHitBox : HitBox
{
    public HitBox explosion;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!collision.gameObject.tag.Contains("Enemy"))
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
