using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragonFireBalls : HitBox
{
    Rigidbody2D[] rb;
    Rigidbody2D parentRb;
    int cnt;
    private void Awake()
    {
        parentRb = GetComponent<Rigidbody2D>();
        rb = GetComponentsInChildren<Rigidbody2D>();
    }
    private void Update()
    {
        cnt = 0;
        foreach (var i in rb)
        {
            if (i)
                i.velocity = parentRb.velocity;
           
        }
        if (transform.childCount==0)
            Destroy(gameObject);
    }
    protected override void OnTriggerEnter2D(Collider2D collision)
    {

    }
}
