using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        
    }

    /*IEnumerator Attack()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z -= Camera.main.transform.position.z;
        Vector3 attackDir = mousepos - PlayerController.singleton.transform.position;
        attackDir = attackDir.normalized;
        hb.transform.position = PlayerController.singleton.transform.position + attackDir;
        hb.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        hb.gameObject.SetActive(false);
    }*/
}
