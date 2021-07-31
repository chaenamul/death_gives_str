using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int hp;
    public int maxHp;
    public int dmg;

    public HitBox hitBox;
    public PlayerController playerController;
    public Skeleton skeleton;
    public Bandit bandit;
    public Ninja ninja;
    public Sniper sniper;
    public Zombie zombie;

    void Awake()
    {
        instance = this;
    }
}
