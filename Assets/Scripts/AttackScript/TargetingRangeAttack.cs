using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingRangeAttack : Attack
{
    float bulletSpeed;
    float aimingDelay;
    Vector3 startPoint;
    public override Vector3 TargetUpdate()
    {
        Vector3 target = new Vector3(0, 0, 0);
        if (subject.tag == "Player")
        {

        }
        else
        {
            target = (GameManager.instance.playerController.transform.position);
        }
        return target;
    }

    public void startPointUpdate(Vector3 startPoint) // Normal이전에 호출 필수
    {
        this.startPoint = startPoint;
        return;
    }


    public override IEnumerator Normal()
    {
        yield return new WaitForSeconds(aimingDelay); // Aiming
        if(subject && subject.activeSelf)
        {
            hb.transform.position = startPoint;
            hb.gameObject.SetActive(true);
            Vector3 target = TargetUpdate();
            hb.GetComponent<Rigidbody2D>().velocity = (target - startPoint).normalized * bulletSpeed;
        }
    }

    public override IEnumerator Skill()
    {
        return null;
    }

    public TargetingRangeAttack(int dmg, float delay, HitBox hitbox, object component, GameObject sub, float hbSpeed, float aimingDel) : base(dmg, delay, hitbox, sub, component)
    {
        aimingDelay = aimingDel;
        bulletSpeed = hbSpeed;
    }
}
