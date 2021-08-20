using System.Collections;
using System.Collections.Generic;
using System.ComponentModel.Design.Serialization;
using System.Dynamic;
using UnityEngine;

public class HitScanRangeAttack : Attack
{
    float aimingDelay;
    LineRenderer route;
    public Color c1 = new Color(1, 0, 0, 1);
    public Color c2 = new Color(1, 1, 1, 0);
    public Color c3;
    private float fade=0.5f;
    private int x=5;
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
        while (x!=0) // 발사 후 궤적 서서히 사라짐
        {
            c3 = new Color(1, 1, 1, fade);
            fade -= 0.1f;
            route.SetColors(c2, c3);
            yield return new WaitForSeconds(0.1f);
            x -= 1;
        }
        x = 5;
        fade = 0.5f;
        if (route)
            route.gameObject.SetActive(false);
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
        {
            route.SetColors(c1, c1);
            route.gameObject.SetActive(true);
        }
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
    }
}
