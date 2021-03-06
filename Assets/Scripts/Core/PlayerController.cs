using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public static PlayerController instance = null;

    [SerializeField]
    public float speed;
    [SerializeField]
    private float jumpForce;

    public bool isGhost { get; private set; } = false;
    public bool gainedInvincible;
    public bool isInvincible;
    private bool dead = false;
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator anim;

    public bool isGrounded;
    public bool canDash = false;
    private bool speedIncreased = false;
    public float dashDistance = 5f;
    private bool checkDash = false;
    private const float checkDashTimeLimit = 0.3f;
    private float dashTimer = 0f;
    private float dashDir = 0f;

    [HideInInspector]
    public int doublecoin = 1;
    [HideInInspector]
    public int coinBoost = 0;
    public string Getability = null;
    public string Whatability;
    public Sprite MonsterImage;

    public HitBox initsword;
    public HitBox initSkill;

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
        Getability = null;
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        attacks = new Dictionary<weapon, Attack>();
        attacks[weapon.InitSword] = new SwordAttack(GameManager.instance.Dmg, 0.25f, initsword, gameObject, this);
        attacks[weapon.InitSkill] = new RangeAttack(GameManager.instance.skillDmg, 3, null, this, gameObject, GameManager.instance.skillSpeed, 0.0f, false, true);
        attackManager = new PlayerAttackManager(attacks[weapon.InitSword], attacks[weapon.InitSkill], initSkill);
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
                case 30001:
                    doublecoin = 2;
                    break;
                case 30002:
                    int ran = Random.Range(0, 3);
                    switch(ran)
                    {
                        case 0:
                            GameManager.instance.hp += 10;
                            break;
                        case 1:
                            GameManager.instance.hp += 30;
                            break;
                        case 2:
                            GameManager.instance.hp += 50;
                            break;
                    }
                    if (GameManager.instance.hp > GameManager.instance.maxHp)
                    {
                        GameManager.instance.hp = GameManager.instance.maxHp;
                    }
                    break;
            }
            GameManager.instance.items.RemoveAt(GameManager.instance.items.Count - 1);
        }

        /// <summary>
        /// ???? ??
        /// y : ?????? ???????????? ????
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
        /// ???? ??
        /// 1 : ?????? 10 ????
        /// 2 : ?????? 10 ????
        /// +(keypad) : ???? ?????? 10 ????
        /// -(keypad) : ???? ?????? 10 ????
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
        /// ?????????? 
        /// 8 : Die
        /// </summary>
        /// 
        if (Input.GetKeyDown(KeyCode.Alpha8))
        {
            isInvincible = false;
            GetDmg(null, 99999999);
        }
    }

    void FixedUpdate()
    {
        Move();
        CheckGround();
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        /*
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

    void CheckGround()
    {
        Debug.DrawRay(rb.position, Vector3.down * 1.5f, Color.red);
        RaycastHit2D rayHit = Physics2D.Raycast(rb.position, Vector3.down, 1.5f, LayerMask.GetMask("Ground"));
        if (rayHit.collider != null)
        {
            if (rayHit.distance < 1.5f)
            {
                isGrounded = true;
                return;
            }
        }
        isGrounded = false;
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
                // ???????? ???????? ???? ???? ???? (?? ???? ????)
                RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.right * dashDir, dashDistance, LayerMask.GetMask("Ground"));
                if (hit.collider != null)
                {
                    // ???? ?? ???? ??????
                    transform.position += Vector3.right * dashDir * (hit.distance - 0.8f);
                }
                else
                {
                    // ???? ?? ???? ???????? ????
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
        if (sub != null)
        {
            Damaged(sub.transform.position);
        }
        if (GameManager.instance.hp <= 0)
        {
            GameManager.instance.hp = 0;
            Die(sub);
        }
    }
    void Die(Enemy sub)
    {
        if (dead) return;
        dead = true;
        
        if (GameManager.instance.items.Count != 0 && GameManager.instance.items[0] == 30004)
        {
            GameManager.instance.items.Clear();
            GameManager.instance.hp = GameManager.instance.maxHp / 10;
            Invincible();
            dead = false;
            return;
        }
        if (GameManager.instance.abilities.Contains("????????"))
        {
            if (!isGhost)
            {
                isGhost = true;
                Invincible();
                GameManager.instance.hp = GameManager.instance.ghostHp;
                spriteRenderer.color = new Color32(255, 255, 255, 95);
                dead = false;
                return;
            }
            else
            {
                spriteRenderer.color = new Color(255,255,255,255);
                isGhost = false;
            }
        }
        dead = true;
        sub?.GiveStr();
        if (sub.abilityName != null)
        {
            Getability = sub.abilityName;
            Whatability = sub.abilityText;
            MonsterImage = sub.monsterImage;
        }
        if (speedIncreased)
        {
            speed /= 1.2f;
            speedIncreased = false;
        }
        gameObject.SetActive(false);
        GameManager.instance.monsterCount = 0;
        doublecoin = 1;
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
                print("???? '????'?? ??????????.");
                break;
            case EnemyType.bandit:
                print("???? '????????'?? ??????????.");
                break;
            case EnemyType.ninja:
                canDash = true;
                print("???? '????'?? ??????????.");
                break;
            case EnemyType.zombie:
                GameManager.instance.maxHp += 20;
                print("???? '????????'?? ??????????.");
                break;
            case EnemyType.shadow:
                print("???? '????????'?? ??????????.");
                break;
            case EnemyType.ranger:
                print("???? '????????'?? ??????????."); // ???? ?????? ???? ???? ????
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
        dead = false;
        GameManager.instance.hp = GameManager.instance.maxHp;
        gameObject.SetActive(true);
        SaveManager.instance.MoveToPrevScene();
        GameObject.Find("Player").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        if (Getability != null)
        {
            GameManager.instance.abilityCheckPanel.GetComponent<AbilityCheck>().abilityNameCheck(Whatability);
            GameManager.instance.abilityCheckPanel.SetActive(true);
            Time.timeScale = 0;
        }
    }

    public void Damaged(Vector2 targetPos)
    {
        int dirx = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rb.AddForce(new Vector2(dirx, 1) * 15, ForceMode2D.Impulse);
    }

    void PlayerAttacked()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySFX("player_hit", 0.7f);
        }
        Invoke("ShowHitimage", 0f);
        Invoke("StopHitimage", 0.3f);
        if (GameManager.instance.hp > 0)
        {
            gameObject.layer = 7;
            inv = true;
            InvokeRepeating("Light", 0f, 0.2f);
            Invoke("StopLight", 2f);
        }
    }

    void Light()
    {
        if (countTime % 2 == 0)
        {
            spriteRenderer.color = new Color32(255, 255, 255, (byte)(isGhost ? 30 : 90));
        }
        else
        {
            spriteRenderer.color = new Color32(255, 255, 255, (byte)(isGhost ? 60 : 180));
        }
        countTime++;
    }

    void StopLight()
    {
        CancelInvoke("Light");
        spriteRenderer.color = new Color32(255, 255, 255, (byte)(isGhost ? 95 : 255));
        countTime = 0;
        inv = false;
        gameObject.layer = 6;
    }

    void ShowHitimage()
    {
        Image i = null;
        GameObject hitImage = GameObject.Find("Hitimage");
        if(hitImage!=null)
            i = hitImage.GetComponent<Image>();
        if (i !=null)
            i.color = new Color(1, 0, 0, 0.2f);
        
    }

    void StopHitimage()
    {
        Image i = null;
        GameObject hitImage = GameObject.Find("Hitimage");
        if (hitImage != null)
            i = hitImage.GetComponent<Image>();
        if (i != null)
            i.color = new Color(1, 0, 0, 0);
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
    public void IncreaseSpeed()
    {
        speed *= 1.2f;
        speedIncreased = true;
        Invoke("EndIncreaseSpeed", 40.0f);
    }
    private void EndIncreaseSpeed()
    {
        if (!speedIncreased)
            return;
        speedIncreased = false;
        speed /= 1.2f;
    }
}
