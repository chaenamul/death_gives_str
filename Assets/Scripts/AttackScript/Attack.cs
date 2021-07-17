using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum attackType
{
    normal,
    skill,
    neither
};

public abstract class Attack
{
    public float attackDamage;
    public float attackDelay;
    public float skillCoolTime;

    private float curNormalDelay = 0;
    private float curSkillDelay = 0;

    public HitBox hb;
    public GameObject subject; 
    
    public abstract IEnumerator Normal();

    public abstract IEnumerator Skill();

    public void DelayUpdate() // 매 프레임 실행 필수
    {
        curNormalDelay -= Time.deltaTime;
        curSkillDelay -= Time.deltaTime;
        curNormalDelay = curNormalDelay < 0 ? 0 : curNormalDelay;
        curSkillDelay = curSkillDelay < 0 ? 0 : curSkillDelay;
    }

    public void Execute(attackType type) 
    {
        switch (type)
        {
            case attackType.skill:
                if (curSkillDelay == 0)
                {
                    CoroutineManager.instance.Coroutine(Skill());
                    curSkillDelay = skillCoolTime;
                }
                break;
            case attackType.normal:
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
    public Attack(float attackdmg, float delay, HitBox hitbox, GameObject sub)
    {
        this.attackDamage = attackdmg;
        this.attackDelay = delay;
        this.hb = hitbox;
        subject = sub;
    }
}