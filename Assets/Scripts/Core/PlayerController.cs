using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    public float speed;
    [SerializeField]
    public float dashSpeed;
    [SerializeField]
    private float jumpForce;

    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    public bool isGrounded;
    public bool canDash = false;
    private bool checkDash = false;
    private float dashTimer = 0.3f;

    public HitBox Initsword;
    private int countTime = 0;
    public bool inv = false;
    private bool cameraRock = false;
    private enum weapon
    {
        InitSword
    };
    private Dictionary<weapon, Attack> weapons;
    private PlayerAttackManager attackManager;

    void Awake()
    {
        var obj = FindObjectsOfType<PlayerController>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        weapons = new Dictionary<weapon, Attack>();
        weapons[weapon.InitSword] = new SwordAttack(GameManager.instance.dmg, 1, Initsword, gameObject, this);
        attackManager = new PlayerAttackManager(weapons[weapon.InitSword]);
    }
    void Update()
    {
        Jump();
        Flip();
        if (canDash)
        {
            Dash();
        }

        attackManager.weaponType.DelayUpdate();
        attackManager.AttackUpdate();

        if (Input.GetKeyDown(KeyCode.E) && GameManager.instance.items.Count != 0)
        {
            switch (GameManager.instance.items[GameManager.instance.items.Count - 1])
            {
                case 10001:
                    GameManager.instance.hp += GameManager.instance.maxHp * 3 / 10;
                    if (GameManager.instance.hp > GameManager.instance.maxHp)
                    {
                        GameManager.instance.hp = GameManager.instance.maxHp;
                    }
                    print("ü�� 30% ��� ȸ��");
                    break;
                case 10002:
                    GameManager.instance.hp += GameManager.instance.maxHp * 5 / 10;
                    print("ü�� 50% ��� ȸ��");
                    if (GameManager.instance.hp > GameManager.instance.maxHp)
                    {
                        GameManager.instance.hp = GameManager.instance.maxHp;
                    }
                    break;
                case 10003:
                    GameManager.instance.hp += GameManager.instance.maxHp * 7 / 10;
                    print("ü�� 70% ��� ȸ��");
                    if (GameManager.instance.hp > GameManager.instance.maxHp)
                    {
                        GameManager.instance.hp = GameManager.instance.maxHp;
                    }
                    break;
                case 10004:
                    GameManager.instance.hp = GameManager.instance.maxHp;
                    print("ü�� 100% ��� ȸ��");
                    break;
                case 20001:
                    GameManager.instance.life++;
                    print("��� �ϳ� �߰�");
                    break;
            }
            GameManager.instance.items.RemoveAt(GameManager.instance.items.Count - 1);
        }

        /// <summary>
        /// �ӽ� Ű
        /// y������ ī�޶� �÷��̾�� ����
        /// </summary>
        if (Input.GetKeyDown(KeyCode.Y))
        {
            cameraRock = !cameraRock;
        }
        if (cameraRock)
        {
            Camera.main.transform.position = transform.position - new Vector3(0,0,transform.position.z - Camera.main.transform.position.z);
        }
        /// <summary>
        /// �ӽ� Ű
        /// 2������ ������ 10���� 1������ 10����
        /// </summary>
        /// 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.instance.dmg -= 10;
            weapons[weapon.InitSword].UpdateDamage(GameManager.instance.dmg);
            Debug.Log("DMG:" + GameManager.instance.dmg);
        
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.instance.dmg += 10;
            weapons[weapon.InitSword].UpdateDamage(GameManager.instance.dmg);
            Debug.Log("DMG:" + GameManager.instance.dmg);
        }

        /// <summary>
        /// �����ڵ��� 8 ==> Die ����
        /// </summary>
        /// 
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            Die();
        }
    }
    void FixedUpdate()
    {
        Move();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            isGrounded = true;
        }/*
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
        if (collision.gameObject.tag == "EnemyNinja")
        {
            GameManager.instance.hp -= GameManager.instance.ninja.dmg;
            Damaged(collision.transform.position);
            if (GameManager.instance.hp <= 0)
            {
                Die(EnemyType.ninja);
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
        }*/
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
        /*
        if (collision.gameObject.tag == "EnemySkeleton")
        {
            GameManager.instance.hp -= GameManager.instance.skeleton.dmg;
            if (GameManager.instance.hp <= 0)
            {
                Die(EnemyType.skeleton);
            }
        }
        if (collision.gameObject.tag == "EnemyNinja")
        {
            GameManager.instance.hp -= GameManager.instance.ninja.dmg;
            if (GameManager.instance.hp <= 0)
            {
                Die(EnemyType.ninja);
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
        }*/
    }

    void Move()
    {
        float dx = Input.GetAxisRaw("Horizontal");
        rb.AddForce(Vector2.right * dx, ForceMode2D.Impulse);

        if (rb.velocity.x > speed)
        {
            rb.velocity = new Vector2(speed, rb.velocity.y);
        }
        else if (rb.velocity.x < -speed)
        {
            rb.velocity = new Vector2(-speed, rb.velocity.y);
        }

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

    void Dash()
    {
        if (Input.GetButtonUp("Horizontal"))
        {
            checkDash = true;
        }

        if (checkDash)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                checkDash = false;
                dashTimer = 0.3f;
            }
        }

        if (Input.GetButtonDown("Horizontal") && !checkDash)
        {
            speed = 10f;
        }
        else if (Input.GetButtonDown("Horizontal") && checkDash)
        {
            speed = dashSpeed;
        }
    }

    public void GetDmg(Enemy sub, int dmg)
    {
        PlayerAttacked();
        GameManager.instance.hp -= dmg;
        Damaged(sub.transform.position);
        if (GameManager.instance.hp <= 0)
        {
            Die();
            sub.GiveStr();
        }
    }
    void Die()
    {
        gameObject.SetActive(false);
        GameManager.instance.life -= 1;
        if(GameManager.instance.life == 0)
        {
            GameOver();
            return;
        }
        Invoke("Revive", 2f);
        /*
        switch(type)
        {
            case EnemyType.skeleton:
                GameManager.instance.dmg += 5;
                print("�ɷ� '����'�� ������ϴ�.");
                break;
            case EnemyType.bandit:
                print("�ɷ� '�Ҹ�ġ��'�� ������ϴ�.");
                break;
            case EnemyType.ninja:
                canDash = true;
                print("�ɷ� '�뽬'�� ������ϴ�.");
                break;
            case EnemyType.zombie:
                GameManager.instance.maxHp += 20;
                print("�ɷ� '�ѵ�����'�� ������ϴ�.");
                break;
            case EnemyType.shadow:
                print("�ɷ� '���ð���'�� ������ϴ�.");
                break;
            case EnemyType.ranger:
                print("�ɷ� 'ȭ�ళ��'�� ������ϴ�."); // ��ų ��Ÿ� ���� ���� �ʿ�
                break;
            default:
                break;
        }*/
    }

    void GameOver()
    {
        GameManager.instance.gameOverPanel.SetActive(true);
    }

    void Revive()
    {
        GameManager.instance.hp = GameManager.instance.maxHp;
        gameObject.SetActive(true);
        SaveManager.instance.MoveToPrevScene();
    }

    void Damaged(Vector2 targetPos)
    {
        int dirx = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rb.AddForce(new Vector2(dirx, 1) * 15, ForceMode2D.Impulse);
    }
    private void PlayerAttacked()
    {
        inv = true;
        Invoke("ShowHitimage", 0f);
        Invoke("StopHitimage", 0.1f);
        InvokeRepeating("Light", 0f, 0.2f);
        Invoke("StopLight", 3f);
    }

    void Light()
    {
        if (countTime % 2 == 0)
        {
            GameManager.instance.playerController.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 90);
        }
        else
        {
            GameManager.instance.playerController.GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 180);
        }
        countTime++;
    }

    void StopLight()
    {
        CancelInvoke("Light");
        GameObject.Find("Player").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        countTime = 0;
        inv = false;
    }

    void ShowHitimage()
    {
        GameObject.Find("Hitimage").GetComponent<Image>().color = new Color(1, 0, 0, 0.2f);
    }

    void StopHitimage()
    {
        GameObject.Find("Hitimage").GetComponent<Image>().color = new Color(1, 0, 0, 0);
    }
}
