using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Xml.Schema;
using UnityEngine;

public class Dragon : Enemy
{
    // Start is called before the first frame update
    public HitBox thorn;
    public HitBox fireBallHitBox;
    private Rigidbody2D rbThorn;
    private Attack thornAttack;
    private int dmgCount;
    public float fireBallSpeed;
    public float ThornSize;
    public static float stunTime = 2.0f;
    private float fireBallDelay = 0.5f;
    private float fireBallCurDel = 0.0f;
    private float SummonSkeletonDelay = 30.0f;
    private float SummonSkeletonCurDel = 0.0f;
    private int fireBallDmg = 45;
    bool gigantized = false;
    public Skeleton skeleton;
    public GameObject Minions;
    private int MinionCount;
    private bool recognized=false;

    private void Awake()
    {
        MinionCount = Minions.transform.childCount;
        thornAttack = new RangeAttack(20, 8.6f, thorn, this, gameObject, ThornSize/0.3f, 0.3f, false, false, new Vector3(0,1,0));
        rbThorn = thorn.GetComponent<Rigidbody2D>();
    }
    protected override void Start()
    {
        base.Start();
    }
    // Update is called once per frame
    protected override void Update()
    {
        if (!recognized)
        {
            isRecognized();
            return;
        }
        base.Update();
        SummonSkeletonCurDel -= Time.deltaTime;
        if (SummonSkeletonCurDel <= 0)
        {
            SummonSkeleton();
        }
        if (hp <= maxHp / 2 && !gigantized)
        {
            Gigantization();
        }
        fireBallCurDel -= Time.deltaTime;
        thornAttack.DelayUpdate();
        if (thornAttack.watingAttack)
        {
            ((RangeAttack)thornAttack).StartPointUpdate(TraceThornStartPoint());
            thornAttack.Execute(AttackType.normal);
            StartCoroutine(ThornAttackAfter());
        }
        else if(dmgCount>=50 && fireBallCurDel<=0)
        {
            dmgCount -= 50;
            fireBallPattern();
            fireBallCurDel = fireBallDelay;
        }
    }

    public override void GetDmg(int dmg)
    {
        base.GetDmg(dmg);
        dmgCount += dmg;
    }

    private void Gigantization()
    {
        gigantized = true;
        stunTime = 3.0f;
        thornAttack.UpdateDamage(30);
        fireBallDmg = 60;
        transform.position += new Vector3(0, (7.0f / 2.5f - 1) * transform.localScale.y, 0);
        Camera.main.orthographicSize *= 2;
        transform.localScale *= 7.0f / 2.5f;
    }

    private Vector3 TraceThornStartPoint()
    {
        RaycastHit2D hit = Physics2D.Raycast(GameManager.instance.playerController.transform.position, new Vector3(0, -1,0), Mathf.Infinity, 1 << 8);
        return GameManager.instance.playerController.transform.position - new Vector3(0, hit.distance+ThornSize/2f, -3);
    }

    private IEnumerator ThornAttackAfter() 
    {
        yield return new WaitForSeconds(0.6f);
        rbThorn.velocity = new Vector2(0, 0);
        yield return new WaitForSeconds(5.0f);
        thorn.gameObject.SetActive(false);
    }
    
    private void fireBallPattern()
    {
        HitBox hb = Instantiate(fireBallHitBox, transform.parent);
        Attack fireBall = new RangeAttack(fireBallDmg, 0f, hb, this, gameObject, fireBallSpeed, 1.0f, false, true);
        ((RangeAttack)fireBall).StartPointUpdate(transform.position + new Vector3(0, transform.localScale.y + 2, 0));
        fireBall.Execute(AttackType.normal);
    }
    private void SummonSkeleton()
    {
        for(int i = 1; i <= 10; i++)
        {
            Skeleton sk = Instantiate(skeleton);
            sk.transform.position = transform.position - new Vector3(i * 3+3, 0, 0);
        }
        SummonSkeletonCurDel = SummonSkeletonDelay;
    }
    private void isRecognized()
    {
        int cnt=0;
        for(int i = 0; i < MinionCount; i++)
        {
            if (Minions.transform.GetChild(i).gameObject.activeSelf)
            {
                cnt++;
            }
        }
        //Debug.Log(cnt + "\t" + MinionCount + "\t" + MinionCount*0.3);
        if (cnt <= MinionCount*0.3)
        {
            base.Start();
            recognized= true;
        }
        else recognized= false;
    }
}
