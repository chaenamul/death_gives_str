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
            if (PlayerShadow.instance != null)
                PlayerShadow.instance.attack.UpdateDamage(value/5);
            dmg = value;
        }
    }
    public int monsterCount;
    public List<string> abilities;
    public List<int> items;
    public int money;

    public HitBox hitBox;
    public PlayerController playerController;
    public GameObject gameOverPanel;
    public GameObject abilityCheckPanel;
    public int life = 9;
    public int skillDmg;
    public float skillSpeed;
    public int ghostHp;

    private int initSkillDmg;
    private int initHp;
    private int initMaxHp;
    private int initDmg;
    private float initSkillSpeed;
    public float defaultCameraSize { get; private set; }
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
        defaultCameraSize = Camera.main.orthographicSize;
        monsterCount = 0;
        initSkillDmg = skillDmg;
        initHp = hp;
        initMaxHp = maxHp;
        initDmg = dmg;
        abilities = new List<string>();
        items = new List<int>();
        initSkillSpeed = skillSpeed;
        ghostHp = 0;
    }

    public void Initialize()
    {
        skillSpeed = initSkillSpeed;
        abilities.Clear();
        items.Clear();
        hp = initHp;
        maxHp = initMaxHp;
        dmg = initDmg;
        skillDmg = initSkillDmg;
        life = 9;
        ghostHp = 0;
    }
}
