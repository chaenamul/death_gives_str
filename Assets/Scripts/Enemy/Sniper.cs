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

    protected override void Start() 
    {
        base.Start();
        abilityName = "화약개조";
        abilityText = "거리에 따른 마법 공격 추가피해";
        delay = 5.0f;
        hp = 100;
        maxHp = 100;
        dmg = 50;
        
        sniping = new HitScanRangeAttack(dmg, delay, gameObject,this, 3.0f, laser);
    }

    protected override void Update()
    {
        Vector3 hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = hpBarPos;
        getMoneyText.transform.position = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));

        sniping.DelayUpdate();
        sniping.Execute();
    }
    /*
    bool InScreen()
    {
        float whRatio = Screen.width / (float)Screen.height;
        float sizeHRatio = Camera.main.orthographicSize / (float)Screen.height;
        return (math.abs(transform.position.x - Camera.main.transform.position.x) <= Screen.height * whRatio * sizeHRatio) & (math.abs(transform.position.y - Camera.main.transform.position.y) <= Screen.height * sizeHRatio);
    }
    */
    public override void GiveStr()
    {
        base.GiveStr();
        (GameManager.instance.playerController.AttackManager.skillHitBox as RangeLimitedAttackHitBox).range += 1;
    }
}
