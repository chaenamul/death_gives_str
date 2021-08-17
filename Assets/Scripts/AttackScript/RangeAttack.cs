using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeAttack : Attack
{
    float bulletSpeed;
    float aimingDelay;
    Vector3 startPoint;
    bool targetting;
    bool guiding;
    Vector3? direction;
    public override Vector3 TargetUpdate()
    {
        Vector3 target = new Vector3(0, 0, 0);
        if (subject.tag == "Player")
        {
            target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            target.z = 0;
            Debug.Log(target);
        }
        else
        {
            target = (GameManager.instance.playerController.transform.position);
        }
        return target;
    }

    public void StartPointUpdate(Vector3 startPoint) // Normal이전에 호출 필수
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
            Vector3 target;
            if (targetting)
            {
                target = TargetUpdate();
                hb.GetComponent<Rigidbody2D>().velocity = ((Vector2)target - (Vector2)startPoint).normalized * bulletSpeed;
            }
            else
            {
                target = (Vector3)direction;
                hb.GetComponent<Rigidbody2D>().velocity = target.normalized * bulletSpeed;
            }
        }
    }

    public RangeAttack(int dmg, float delay, HitBox hitbox, object component, GameObject sub, float hbSpeed, float aimingDel, bool guiding, bool targetting, Vector3? direction = null) : base(dmg, delay, hitbox, sub, component)
    {
        this.direction = direction;
        this.targetting = targetting;
        this.guiding = guiding;
        aimingDelay = aimingDel;
        bulletSpeed = hbSpeed;
    }
}
