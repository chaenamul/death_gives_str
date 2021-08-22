using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public int hp;
    public int maxHp;
    [SerializeField]
    private int dmg;
    public int Dmg
    {
        get
        {
            return dmg;
        }
        set
        {
            playerController.AttackManager.attack.UpdateDamage(value);
            dmg = value;
        }
    }
    [HideInInspector]
    public int monsterCount;
    public List<string> abilities;
    public List<int> items;
    public int money;

    public HitBox hitBox;
    public PlayerController playerController;
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
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }

        monsterCount = 0;
        initSkillDmg = skillDmg;
        initHp = hp;
        initMaxHp = maxHp;
        initDmg = dmg;
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
