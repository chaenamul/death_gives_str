using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float speed;
    [SerializeField]
    private float power;
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

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Skeleton")
        {
            GameManager.instance.hp -= GameManager.instance.skeleton.dmg;
            if (GameManager.instance.hp <= 0)
            {
                Die(MonsterType.skeleton);
            }
        }
    }

    void Move()
    {
        // 김준하
    }

    void Jump()
    {
        // 김준하
    }

    void Die(MonsterType type)
    {
        switch(type)
        {
            case MonsterType.skeleton:
                power += 5f;
                print("능력 '원한'을 얻었습니다.");
                break;
            default:
                break;
        }
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
