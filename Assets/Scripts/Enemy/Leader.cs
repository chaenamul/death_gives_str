using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Leader : Enemy
{
    [SerializeField]
    private GameObject memberPrefab;

    private float spawnTimer = 7f;

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
            spawnTimer -= Time.deltaTime;
            if (spawnTimer <= 0)
            {
                Spawn();
                spawnTimer = 7f;
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
