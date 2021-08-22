using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;

    [SerializeField]
    public float speed;
    [SerializeField]
    private float jumpForce;

    public bool isDmgBoosted;
    public bool gainedInvincible;
    public bool isInvincible;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    public bool isGrounded;
    public bool canDash = false;
    public float dashDistance = 5f;
    private bool checkDash = false;
    private const float checkDashTimeLimit = 0.3f;
    private float dashTimer = 0f;
    private float dashDir = 0f;

    public HitBox initsword;
    [SerializeField]
    private HitBox initSkill;


    private int countTime = 0;
    public bool inv = false;
    private bool cameraRock = false;
    public enum weapon
    {
        InitSword,
        InitSkill,
    };
    private Dictionary<weapon, Attack> attacks;
    public Dictionary<weapon, Attack> Attacks
    {
        get
        {
            return attacks;
        }
    }
    private PlayerAttackManager attackManager;
    public PlayerAttackManager AttackManager
    {
        get
        {
            return attackManager;
        }
    }
    void Awake()
    {
        DontDestroyOnLoad(gameObject);
        gainedInvincible = false;
        isInvincible = false;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        attacks = new Dictionary<weapon, Attack>();
        attacks[weapon.InitSword] = new SwordAttack(GameManager.instance.Dmg, 0.25f, initsword, gameObject, this);
        attacks[weapon.InitSkill] = new RangeAttack(GameManager.instance.skillDmg, 1, initSkill, this, gameObject, GameManager.instance.skillSpeed, 0.0f, false, true);
        attackManager = new PlayerAttackManager(attacks[weapon.InitSword], attacks[weapon.InitSkill]);
    }

    void Update()
    {
        Jump();
        Flip();
        if (canDash)
        {
            Dash();
        }

        attackManager.attack.DelayUpdate();
        attackManager.skill.DelayUpdate();
        attackManager.AttackUpdate();

        if (Input.GetMouseButtonDown(0))
        {
            StartCoroutine(AttackAnimation());
        }
        if (Input.GetMouseButton(1))
        {
            StartCoroutine(MagicAnimation());
        }

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
        /// y : ī�޶� �÷��̾�� ����
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
        /// 1 : ������ 10 ����
        /// 2 : ������ 10 ����
        /// +(keypad) : ��ų ������ 10 ����
        /// -(keypad) : ��ų ������ 10 ����
        /// </summary>
        /// 
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            GameManager.instance.Dmg -= 10;
            Debug.Log($"Dmg:{GameManager.instance.Dmg}");
        
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            GameManager.instance.Dmg += 10;
            Debug.Log($"Dmg:{GameManager.instance.Dmg}");
        }

        if (Input.GetKeyDown(KeyCode.KeypadPlus))
        {
            GameManager.instance.skillDmg += 10;
            attacks[weapon.InitSkill].UpdateDamage(GameManager.instance.skillDmg);
            Debug.Log($"SkillDmg: {GameManager.instance.skillDmg}");
        }

        if (Input.GetKeyDown(KeyCode.KeypadMinus))
        {
            GameManager.instance.skillDmg -= 10;
            attacks[weapon.InitSkill].UpdateDamage(GameManager.instance.skillDmg);
            Debug.Log($"SkillDmg: {GameManager.instance.skillDmg}");
        }

        /// <summary>
        /// �����ڵ��� 
        /// 8 : Die
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
        if (dashTimer > 0)
        {
            dashTimer -= Time.deltaTime;
            if (dashTimer <= 0)
            {
                checkDash = false;
            }
        }

        if (Input.GetButtonDown("Horizontal"))
        {
            if (checkDash && dashDir == Input.GetAxisRaw("Horizontal"))
            {
                // �ٶ󺸴� �������� ���� �Ÿ� �̵� (�� ���� ����)
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * dashDir, dashDistance, LayerMask.GetMask("Ground"));
                if (hit.collider != null)
                {
                    // �뽬 �� ���� �ε���
                    transform.position += Vector3.right * dashDir * (hit.distance - 0.8f);
                }
                else
                {
                    // �뽬 �� ���� �ε����� ����
                    transform.position += Vector3.right * dashDir * dashDistance;
                }

                checkDash = false;
                dashTimer = 0;
            }
            else
            {
                dashDir = Input.GetAxisRaw("Horizontal");

                checkDash = true;
                dashTimer = checkDashTimeLimit;
            }
        }
    }

    IEnumerator AttackAnimation()
    {
        anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.2f);
        anim.SetBool("isAttacking", false);
    }

    IEnumerator MagicAnimation()
    {
        anim.SetBool("isCasting", true);
        yield return new WaitForSeconds(0.5f);
        anim.SetBool("isCasting", false);
    }

    public void GetDmg(Enemy sub, int dmg)
    {
        if (isInvincible)
        {
            return;
        }
        GameManager.instance.hp -= dmg;
        PlayerAttacked();
        Damaged(sub.transform.position);
        if (GameManager.instance.hp <= 0)
        {
            Die();
            sub.GiveStr();
        }
    }
    void Die()
    {
        if (isDmgBoosted)
        {
            isDmgBoosted = false;
            GameManager.instance.Dmg--;
        }
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

    void PlayerAttacked()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX("player_hit", 0.3f);
        }
        Invoke("ShowHitimage", 0f);
        Invoke("StopHitimage", 0.1f);
        if (GameManager.instance.hp > 0)
        {
            inv = true;
            InvokeRepeating("Light", 0f, 0.2f);
            Invoke("StopLight", 1f);
        }
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
    public void Invincible()
    {
        isInvincible = true;
        Invoke("NotInvincible", 2.0f);
    }
    void NotInvincible()
    {
        isInvincible = false;
    }
}
