using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : Attack
{
    public override Vector3 TargetUpdate()
    {
        Vector3 attackDir = new Vector3(0,0,0);
        if(subject.tag == "Player")
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z -= Camera.main.transform.position.z;
            attackDir = (mousepos - subject.transform.position).normalized * 1.5f;
        }
        else if(subject.tag == "PlayerShadow")
        {
            if(PlayerShadow.instance)
                attackDir = PlayerShadow.instance.attackDir;
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
            SoundManager.Instance.PlaySFX("player_attack", 0.7f);
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
