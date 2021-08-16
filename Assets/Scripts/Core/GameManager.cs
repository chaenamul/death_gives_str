using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int hp;
    public int maxHp;
    public int dmg;
    public List<string> abilities;
    public List<int> items;
    public int money;

    public HitBox hitBox;
    public PlayerController playerController;
    public Skeleton skeleton;
    public Bandit bandit;
    public Ninja ninja;
    public Sniper sniper;
    public Zombie zombie;
    public Shadow shadow;
    public GameObject gameOverPanel;
    public int life = 9;
    public int skillDmg;
    public float skillSpeed;

    private int initSkillDmg;
    private int initHp;
    private int initMaxHp;
    private int initDmg;
    private float initSkillSpeed;

    void Awake()
    {
        initSkillDmg = skillDmg;
        initHp = hp;
        initMaxHp = maxHp;
        initDmg = dmg;
        instance = this;
        abilities = new List<string>();
        initSkillSpeed = skillSpeed;
    }

    public void Initialize()
    {
        skillSpeed = initSkillSpeed;
        abilities.Clear();
        hp = initHp;
        maxHp = initMaxHp;
        dmg = initDmg;
        skillDmg = initSkillDmg;
        life = 9;
    }
}
