using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HitScanRangeAttack : Attack
{
    float aimingDelay;
    public override Vector3 TargetUpdate()
    {
        Vector3 attackDir = new Vector3(0,0,0);
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
        if (subject && subject.activeSelf)
        {
            Vector3 target = TargetUpdate();
            RaycastHit2D hit = Physics2D.Raycast(subject.transform.position, target - subject.transform.position, Mathf.Infinity, ~(1 << subject.layer));
            Debug.DrawRay(subject.transform.position, (target - subject.transform.position).normalized * hit.distance, Color.red, 0.3f);
            Debug.Log(hit.distance);
            if (hit)
            {
                if (subject.tag == "Player" && hit.transform.gameObject.tag.Contains("Enemy"))
                {

                }
                else if (subject.tag.Contains("Enemy") && hit.transform.gameObject.tag == "Player" && !GameManager.instance.playerController.inv)
                {
                    Debug.Log("Hit");
                    GameManager.instance.playerController.GetDmg((Enemy)subjectClass, attackDamage);
                }
            }
        }
    }

    public override IEnumerator Skill()
    {
        return null;
    }

    public HitScanRangeAttack(int dmg, float delay, GameObject sub, object component, float aimingDel) : base(dmg, delay, null, sub, component)
    {
        hb = null;
        aimingDelay = aimingDel;
    }
}
