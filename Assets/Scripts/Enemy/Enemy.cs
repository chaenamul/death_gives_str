using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
    public int dmg;
    public float speed;
    public float sight;
    protected string abilityName; // ���߿� ���� Ŭ���� ����°� ������
    public GameObject target;
    protected virtual void Die()
    {
        gameObject.SetActive(false);
    }
    public virtual void GetDmg(int dmg)
    {
        hp -= dmg;
        if (hp <= 0) Die();
    }

    public virtual void GiveStr()
    {
        GameManager.instance.abilities.Add(abilityName);
    }
}
