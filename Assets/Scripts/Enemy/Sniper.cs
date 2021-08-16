using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class Sniper : Enemy
{
    [SerializeField]
    private float delay;
    [SerializeField]
    private LineRenderer laser;
    private Attack sniping;
    private Rigidbody2D rb;

    protected override void Start() 
    {
        base.Start();
        abilityName = "화약개조";
        rb = GetComponent<Rigidbody2D>();
        delay = 5.0f;
        hp = 100;
        maxHp = 100;
        dmg = 50;
        
        sniping = new HitScanRangeAttack(dmg, delay, gameObject,this, 3.0f, laser);
    }

    protected override void Update()
    {
        base.Update();
        sniping.DelayUpdate();
        if (InScreen())
        {
            sniping.Execute();
        }
    }

    bool InScreen()
    {
        float whRatio = Screen.width / (float)Screen.height;
        float sizeHRatio = Camera.main.orthographicSize / (float)Screen.height;
        return (math.abs(transform.position.x - Camera.main.transform.position.x) <= Screen.height * whRatio * sizeHRatio) & (math.abs(transform.position.y - Camera.main.transform.position.y) <= Screen.height * sizeHRatio);
    }

    public override void GiveStr()
    {
        base.GiveStr();
        ///마법 구현 후 구현 필요
    }
}
