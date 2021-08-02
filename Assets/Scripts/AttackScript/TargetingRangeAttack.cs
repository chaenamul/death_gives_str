using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetingRangeAttack : Attack
{
    float bulletSpeed;
    float aimingDelay;
    public override Vector3 TargetUpdate()
    {
        Vector3 attackDir = new Vector3(0, 0, 0);
        if (subject.tag == "Player")
        {

        }
        else
        {
            attackDir = (GameManager.instance.playerController.transform.position);
        }
        return attackDir;
    }

    public override IEnumerator Normal()
    {
        yield return new WaitForSeconds(aimingDelay); // Aiming
        hb.gameObject.SetActive(true);
        Vector3 target = TargetUpdate();
        hb.transform.position = subject.transform.position + (target - subject.transform.position).normalized;
        hb.GetComponent<Rigidbody2D>().velocity = (target - subject.transform.position).normalized * bulletSpeed;
    }

    public override IEnumerator Skill()
    {
        return null;
    }

    public TargetingRangeAttack(int dmg, float delay, HitBox hitbox, GameObject sub, float hbSpeed, float aimingDel, Vector3 target) : base(dmg, delay, hitbox, sub)
    {

        aimingDelay = aimingDel;
        bulletSpeed = hbSpeed;
    }
}
