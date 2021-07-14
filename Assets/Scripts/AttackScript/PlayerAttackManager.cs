using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.UIElements;

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
            weaponType.Execute(Attack.attackType.normal);
        }
        else if (Input.GetMouseButtonDown(1))
        {
            weaponType.Execute(Attack.attackType.skill);
        }
        else
        {
            weaponType.Execute(Attack.attackType.neither);
        }
        
    }

}
