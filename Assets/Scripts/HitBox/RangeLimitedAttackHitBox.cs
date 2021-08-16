using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeLimitedAttackHitBox : RangeAttackHitBox
{
    private Vector3 startPoint;
    [SerializeField]
    private float range;
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
            gameObject.SetActive(false);
        }
    }
}
