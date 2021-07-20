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
        float xAxis = Input.GetAxisRaw("Horizontal");
        Vector3 move = new Vector3(xAxis, 0, 0);
        transform.position += move * speed * Time.deltaTime;
    }

    void Jump()
    {
        Rigidbody2D rigid;
        rigid = GetComponent<Rigidbody2D>();

        RaycastHit2D rayhit;
        rayhit = Physics2D.Raycast(rigid.position , Vector3.down, 2 , LayerMask.GetMask("Ground"));
        
        float jumppower = 12;
        if (rayhit.collider != null)
            if (Input.GetButtonDown("Jump") && rayhit.distance < 1.1f )
            {
                rigid.AddForce(Vector2.up * jumppower, ForceMode2D.Impulse);
            }
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
