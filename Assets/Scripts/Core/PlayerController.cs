using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    private float jumpForce;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    public bool isGrounded;

    public HitBox Initsword;
    private enum weapon
    {
        InitSword
    };
    private Dictionary<weapon, Attack> weapons;
    private PlayerAttackManager attackManager;

    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        weapons = new Dictionary<weapon, Attack>();
        weapons[weapon.InitSword] = new SwordAttack(5, 1, Initsword, gameObject);
        attackManager = new PlayerAttackManager(weapons[weapon.InitSword]);
    }

    void Update()
    {
        Move();
        Jump();
        Flip();

        attackManager.weaponType.DelayUpdate();
        attackManager.AttackUpdate();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }
        if (collision.gameObject.tag == "EnemySkeleton")
        {
            GameManager.instance.hp -= GameManager.instance.skeleton.dmg;
            Damaged(collision.transform.position);
            if (GameManager.instance.hp <= 0)
            {
                Die(EnemyType.skeleton);
            }
        }
        if (collision.gameObject.tag == "EnemyBandit")
        {
            GameManager.instance.hp -= GameManager.instance.bandit.dmg;
            Damaged(collision.transform.position);
            if (GameManager.instance.hp <= 0)
            {
                Die(EnemyType.bandit);
            }
        }
        if (collision.gameObject.tag == "EnemyZombie")
        {
            GameManager.instance.hp -= GameManager.instance.zombie.dmg;
            Damaged(collision.transform.position);
            if (GameManager.instance.hp <= 0)
            {
                Die(EnemyType.zombie);
            }
        }
        if (collision.gameObject.tag == "EnemyShadow") {
            GameManager.instance.hp -= GameManager.instance.shadow.dmg;
            Damaged(collision.transform.position);
            if (GameManager.instance.hp <= 0) {
                Die(EnemyType.shadow);
            }
        }
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = false;
        }
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "EnemySkeleton")
        {
            GameManager.instance.hp -= GameManager.instance.skeleton.dmg;
            if (GameManager.instance.hp <= 0)
            {
                Die(EnemyType.skeleton);
            }
        }
        if (collision.gameObject.tag == "EnemyZombie")
        {
            GameManager.instance.hp -= GameManager.instance.zombie.dmg;
            if(GameManager.instance.hp <= 0)
            {
                Die(EnemyType.zombie);
            }
        }
        if (collision.gameObject.tag == "EnemyShadow") {
            GameManager.instance.hp -= GameManager.instance.shadow.dmg;
            Damaged(collision.transform.position);
            if (GameManager.instance.hp <= 0) {
                Die(EnemyType.shadow);
            }
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Door" && Input.GetKey(KeyCode.UpArrow))
        {
            SceneManager.LoadScene("Room1");
        }
    }

    void Move()
    {
        float dx = Input.GetAxisRaw("Horizontal");
        transform.Translate(new Vector3(dx, 0, 0) * speed * Time.deltaTime);
        if (dx == 0)
        {
            anim.SetBool("isWalking", false);
        }
        else
        {
            anim.SetBool("isWalking", true);
        }
    }

    void Jump()
    {
        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            rb.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
        }
    }

    void Flip()
    {
        if (Input.GetButton("Horizontal"))
        {
            spriteRenderer.flipX = Input.GetAxisRaw("Horizontal") == 1;
        }
    }

    void Die(EnemyType type)
    {
        gameObject.SetActive(false);

        switch(type)
        {
            case EnemyType.skeleton:
                GameManager.instance.dmg += 5;
                print("능력 '원한'을 얻었습니다.");
                break;
            case EnemyType.bandit:
                print("능력 '소매치기'를 얻었습니다.");
                break;
            case EnemyType.zombie:
                GameManager.instance.maxHp += 20;
                print("능력 '한도증가'를 얻었습니다.");
                break;
            case EnemyType.shadow:
                print("능력 '도플갱어'를 얻었습니다.");
                break;
            case EnemyType.ranger:
                print("능력 '화약개조'를 얻었습니다."); // 스킬 사거리 증가 구현 필요
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

    void Damaged(Vector2 targetPos)
    {
        int dirx = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rb.AddForce(new Vector2(dirx, 1) * 3, ForceMode2D.Impulse);
    }
}
