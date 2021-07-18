using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttackManager
{
    public Attack weaponType;
    public PlayerAttackManager(Attack init)
    {
        this.weaponType = init;
    }

    public void TypeChange(Attack newWeapon)
    {
        this.weaponType = newWeapon;
    }
    
    public void AttackUpdate()
    {
        if (Input.GetMouseButtonDown(0))
        {
            weaponType.Execute(AttackType.normal);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            weaponType.Execute(AttackType.skill);
        }
        else
        {
            weaponType.Execute(AttackType.neither);
        }
        
    }

}
