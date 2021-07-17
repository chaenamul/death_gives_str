using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public HitBox Initsword;
    private enum weapon
    {
        InitSword
    };
    private Dictionary<weapon, Attack> weapons;
    private PlayerAttackManager attackManager;
        
    void Awake()
    {
        weapons = new Dictionary<weapon, Attack>();
        weapons[weapon.InitSword] = new SwordAttack(5, 1, Initsword, gameObject);
        attackManager = new PlayerAttackManager(weapons[weapon.InitSword]);
    }

    void Update()
    {
        Move();
        Jump();
        attackManager.weaponType.DelayUpdate();
        attackManager.AttackUpdate();
    }

    void Move()
    {
        // ±Ë¡ÿ«œ
    }

    void Jump()
    {
        // ±Ë¡ÿ«œ
    }

    void Die()
    {
        
    }

    void Revive()
    {
        string data = SaveManager.instance.popData();
        if (data != null)
        {
            SceneManager.LoadScene(data);
        }
    }

}
