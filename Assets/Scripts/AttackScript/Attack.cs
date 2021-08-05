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
    public int attackDamage;
    public float attackDelay;
    public float skillCoolTime;

    private float curNormalDelay = 0;
    private float curSkillDelay = 0;

    public HitBox hb;
    public GameObject subject;
    protected object subjectClass;

    public abstract Vector3 TargetUpdate();

    public abstract IEnumerator Normal();

    public abstract IEnumerator Skill();

    public void DelayUpdate() // �� ������ ���� �ʼ�
    {
        curNormalDelay -= Time.deltaTime;
        curSkillDelay -= Time.deltaTime;
        curNormalDelay = curNormalDelay < 0 ? 0 : curNormalDelay;
        curSkillDelay = curSkillDelay < 0 ? 0 : curSkillDelay;
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
            hb.dmg = attackdmg;
            hb.subject = component;
        }
        subject = sub;
    }
}