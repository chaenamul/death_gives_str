using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager
{
    public Attack attack;
    public Attack skill;
    public PlayerAttackManager(Attack init, Attack skill)
    {
        this.attack = init;
        this.skill = skill;
    }

    public void AttackChange(Attack newAttack)
    {
        this.attack = newAttack;
    }
    public void SkillChange(Attack newSkill)
    {
        this.skill = newSkill;
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
