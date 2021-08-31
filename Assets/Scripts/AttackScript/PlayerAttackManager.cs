using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager
{
    public Attack attack;
    public Attack skill;
    public HitBox skillHitBox { get; private set; }
    public PlayerAttackManager(Attack init, Attack skill, HitBox skillHitBox)
    {
        this.attack = init;
        this.skill = skill;
        this.skillHitBox = skillHitBox;
    }

    public void AttackChange(Attack newAttack)
    {
        this.attack = newAttack;
    }
    public void SkillChange(Attack newSkill, HitBox skillHitBox)
    {
        this.skill = newSkill;
        this.skillHitBox = skillHitBox;
    }
    
    public void AttackUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            attack.Execute();
        }
        else if (Input.GetMouseButtonDown(1))
        {
            if(skill is RangeAttack)
            {
                RangeAttackHitBox newHitBox = GameObject.Instantiate(skillHitBox as RangeAttackHitBox);
                newHitBox.isDisposable = true;
                skill.HitBoxUpdate(newHitBox);  
                ((RangeAttack)skill).StartPointUpdate(GameManager.instance.playerController.transform.position);
            }
            if (GameManager.instance.playerController.gainedInvincible)
            {
                GameManager.instance.playerController.Invincible();
            }
            skill.Execute();
        }
    }

}
