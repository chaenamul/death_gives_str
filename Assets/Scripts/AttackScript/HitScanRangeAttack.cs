using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Dynamic;
using UnityEngine;

public class HitScanRangeAttack : Attack
{
    float aimingDelay;
    LineRenderer route;
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
        route.SetPosition(0, subject.transform.position);
        Debug.Log(subject.transform.position);
        yield return CoroutineManager.instance.StartCoroutine(Aiming()); // Aiming
        if (subject && subject.activeSelf)
        {
            Vector3 target = TargetUpdate();
            RaycastHit2D hit = Physics2D.Raycast(subject.transform.position, target - subject.transform.position, Mathf.Infinity, ~(1 << subject.layer));
            
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


    public HitScanRangeAttack(int dmg, float delay, GameObject sub, object component, float aimingDel, LineRenderer route) : base(dmg, delay, null, sub, component)
    {
        hb = null;
        aimingDelay = aimingDel;
        this.route = route;
    }
    private IEnumerator Aiming()
    {
        Transform tr = subject.transform;
        if(route)
            route.gameObject.SetActive(true);
        float time = 0.0f;
        RaycastHit2D hit;
        while (time < aimingDelay)
        {
            route.SetPosition(0, tr.position);
            Vector3 target = TargetUpdate();
            time += Time.deltaTime;
            hit = Physics2D.Raycast(tr.position, target - tr.position, Mathf.Infinity, ~(1 << subject.layer));
            if(route)
                route.SetPosition(1, tr.position + (target + new Vector3(0,0.3f,0) - tr.position).normalized * (hit.distance+0.5f));
            yield return null;
        }
        if(route)
            route.gameObject.SetActive(false);
    }
}
