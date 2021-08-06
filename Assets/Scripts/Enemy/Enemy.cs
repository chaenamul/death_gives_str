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
    ranger
};

public class Enemy : MonoBehaviour
{
    public int hp;
    public int maxHp;
    public int dmg;
    public float speed;
    public float sight;
    protected string abilityName; // 나중에 새로 클래스 만드는게 좋을듯
    public GameObject target;
    public GameObject hpBarPrefab;
    public GameObject canvas;
    private Image nowHpBar;
    public float height = 1.5f;

    private RectTransform hpBar;

    protected virtual void Start()
    {
        hpBar = Instantiate(hpBarPrefab, canvas.transform).GetComponent<RectTransform>();
        nowHpBar = hpBar.transform.GetChild(0).GetComponent<Image>();
    }

    protected virtual void Update()
    {
        Vector3 hpBarPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y + height, 0));
        hpBar.position = hpBarPos;
    }

    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }

    public virtual void GetDmg(int dmg)
    {
        hp -= dmg;
        nowHpBar.fillAmount = (float)hp / (float)maxHp;
        if (hp <= 0)
        {
            Destroy(hpBar.gameObject);
            Die();
        }
    }

    public virtual void GiveStr()
    {
        GameManager.instance.abilities.Add(abilityName);
    }
}
