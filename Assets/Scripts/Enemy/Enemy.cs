using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    skeleton,
    bandit,
    ranger
};

public class Enemy : MonoBehaviour
{
    public int hp;
    public int dmg;
    public float speed;
    public float sight;

    public GameObject target;
}
