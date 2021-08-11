using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum AttackType
{
    normal,
    skill,
    neither
};

public abstract class Attack
{
    protected int attackDamage;
    public float attackDelay;
    public float skillCoolTime;
    public bool watingAttack = true;
    private float curNormalDelay = 0;
    private float curSkillDelay = 0;

    public HitBox hb;
    public GameObject subject;
    protected object subjectClass;

    public abstract Vector3 TargetUpdate();
    public abstract IEnumerator Normal();

    public abstract IEnumerator Skill();

    public void UpdateDamage(int dmg)
    {
        attackDamage = dmg;
        hb.dmg = dmg;
    }

    public void DelayUpdate() // 매 프레임 실행 필수
    {
        curNormalDelay -= Time.deltaTime;
        if (curNormalDelay <= 0)
        {
            curNormalDelay = 0;
            watingAttack = true;
        }
    }

    public void Execute(AttackType type) 
    {
        switch (type)
        {
            case AttackType.skill:
                if (curSkillDelay == 0)
                {
                    CoroutineManager.instance.Coroutine(Skill());
                    curSkillDelay = skillCoolTime;
                }
                break;
            case AttackType.normal:
                if(curNormalDelay == 0)
                {
                    CoroutineManager.instance.Coroutine(Normal());
                    curNormalDelay = attackDelay;
                    watingAttack = false;
                }
                break;
            default:
                break;
        }
    }

    public Attack(int attackdmg, float delay, HitBox hitbox, GameObject sub, object component)
    {
        this.attackDamage = attackdmg;
        this.attackDelay = delay;
        this.hb = hitbox;
        this.subjectClass = component;
        if (hb)
        {
            hb.dmg = this.attackDamage;
            hb.subject = component;
        }
        subject = sub;
    }
}