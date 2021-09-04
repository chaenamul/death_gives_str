using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : Enemy
{
    [SerializeField]
    private GameObject memberPrefab;
    [SerializeField]
    private GameObject warningZone;

    private float spawnTimer = 4.5f;

    protected override void Start()
    {
        base.Start();

        abilityName = "����";
        abilityText = "��ų ��� �� 2�ʰ� ��� ������ ����ȭ";
    }

    protected override void Update()
    {
        base.Update();
        FindPlayer();
    }

    void FindPlayer()
    {
        if (Vector3.Distance(GameManager.instance.playerController.transform.position, transform.position) <= sight)
        {
            if (0 < spawnTimer && spawnTimer <= 3)
            {
                warningZone.SetActive(true);
            }
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                warningZone.SetActive(false);
                Spawn();
                spawnTimer = 4.5f;
            }
        }
    }

    void Spawn()
    {
        for (int i = -3; i <= 3; i++)
        {
            var member = Instantiate(memberPrefab);
            member.transform.position = GameManager.instance.playerController.transform.position + new Vector3(i * 3, 3f, 0);
        }
    }

    public override void GiveStr()
    {
        base.GiveStr();
        GameManager.instance.playerController.gainedInvincible = true;
    }
}
