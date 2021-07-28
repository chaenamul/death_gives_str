using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : Attack
{
    public override Vector3 TargetUpdate()
    {
        Vector3 attackDir;
        if(subject.tag == "Player")
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z -= Camera.main.transform.position.z;
            attackDir = (mousepos - subject.transform.position).normalized;
        }
        else
        {
            attackDir = (GameManager.instance.playerController.transform.position - subject.transform.position).normalized;
        }
        return subject.transform.position + attackDir;
    }


    public override IEnumerator Normal()
    {
        hb.transform.position = TargetUpdate();
        hb.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hb.gameObject.SetActive(false);
    }

    public override IEnumerator Skill()
    {
        //기획 나오면 수정
        return null;
    }

    public SwordAttack(float attackdmg, float delay, HitBox sword, GameObject subject) : base(attackdmg, delay, sword, subject)
    {
        
    }
}
