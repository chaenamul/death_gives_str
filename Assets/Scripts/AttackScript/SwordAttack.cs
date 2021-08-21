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
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX("sword_attack_1", 0.3f);
        }
        yield return new WaitForSeconds(0.1f);
        if (hb)
        {
            hb.gameObject.SetActive(false);
        }
    }


    public SwordAttack(int attackdmg, float delay, HitBox sword, GameObject subject, object component) : base(attackdmg, delay, sword, subject, component)
    {
        
    }
}
