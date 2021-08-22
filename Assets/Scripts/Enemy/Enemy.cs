using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum EnemyType
{
    skeleton,
    bandit,
    ninja,
    sniper,
    zombie,
    shadow,
    ranger,
    bat
};

public class Enemy : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public int dmg;
    public float speed;
    public float sight;
    protected string abilityName; // 나중에 새로 클래스 만드는게 좋을듯
    public GameObject hpBarPrefab;
    private GameObject canvas;
    private Image nowHpBar;
    public float height = 1.5f;
    private Vector3 CameraPos;

    protected Rigidbody2D rb;
    protected Animator anim;
    private SpriteRenderer spriteRenderer;
    private RectTransform hpBar;

    protected virtual void Awake()
    {
        GameManager.instance.monsterCount += 1;
    }
    protected virtual void Start()
    {
        canvas = GameObject.Find("Canvas");
        hpBar = Instantiate(hpBarPrefab, canvas.transform).GetComponent<RectTransform>();
        nowHpBar = hpBar.transform.GetChild(0).GetComponent<Image>();
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected virtual void Update()
    {
        Vector3 hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = hpBarPos;
        Flip();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            GameManager.instance.playerController.GetDmg(this, dmg);
        }
    }

    void Flip()
    {
        spriteRenderer.flipX = rb.velocity.x > 0;
    }

    protected virtual void Die()
    {
        GameManager.instance.monsterCount -= 1;
        GameManager.instance.money += Random.Range(2, 5);
        gameObject.SetActive(false);
    }

    public virtual void GetDmg(int dmg)
    {
        CameraPos = Camera.main.transform.position;
        InvokeRepeating("Shaking", 0f, 0.005f);
        Invoke("StopShaking", 0.2f);
        Damaged(GameManager.instance.playerController.transform.position);
        hp -= dmg;
        nowHpBar.fillAmount = (float)hp / (float)maxHp;
        if (hp <= 0)
        {
            Destroy(hpBar.gameObject);
            Die();
        }
    }

    void Damaged(Vector2 targetPos)
    {
        int dirx = transform.position.x - targetPos.x > 0 ? 1 : -1;
        rb.AddForce(new Vector2(dirx, 1) * 3, ForceMode2D.Impulse);
        print(dirx);
    }

    public virtual void GiveStr()
    {
        GameManager.instance.abilities.Add(abilityName);
    }
    private void Shaking()
    {
        Camera.main.transform.position = Random.insideUnitSphere * 0.05f + Camera.main.GetComponent<CameraController>().CameraShake;
    }

    private void StopShaking()
    {
        CancelInvoke("Shaking");
        CameraPos = Camera.main.GetComponent<CameraController>().CameraShake;
        CameraPos.y = 0f;
        Camera.main.transform.position = CameraPos;
    }
    
}
