using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeLimitedAttackHitBox : RangeAttackHitBox
{
    private Vector3 startPoint;
    public float range;
    protected override void OnTriggerEnter2D(Collider2D collision)
    {
        base.OnTriggerEnter2D(collision);
    }
    private void OnEnable()
    {
        startPoint = GameManager.instance.playerController.transform.position;
    }
    private void Update()
    {
        if(Vector2.Distance(startPoint, transform.position) > range)
        {
            if (!isDisposable)
                gameObject.SetActive(false);
            else
                Destroy(gameObject);
        }
        if(subject is PlayerController && GameManager.instance.abilities.Contains("화약개조"))
        {
            dmg = GameManager.instance.playerController.AttackManager.skill.attackDamage + (int)Vector2.Distance(startPoint, transform.position);
        }
    }
}
