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
        GameManager.instance.money += Random.Range(2, 5);
        gameObject.SetActive(false);
    }

    public virtual void GetDmg(int dmg)
    {
        //InvokeRepeating("Shaking", 0f, 0.005f);
        //Invoke("StopShaking", 0.2f);
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
    /* 카메라 위치가 달라질 때 버그 수정 필요
    private void Shaking()
    {
        Camera.main.transform.position = Random.insideUnitSphere * 0.05f + new Vector3(0f, 0f, -10f);
    }

    private void StopShaking()
    {
        CancelInvoke("Shaking");
        Camera.main.transform.position = new Vector3(0f, 0f, -10f);
    }
    */
}
