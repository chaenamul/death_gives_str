using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthKit : Item
{
    protected override void Update()
    {
        base.Update();

        if (isColliding)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (GameManager.instance.money >= cost)
                {
                    BuyandApply();
                }
                else
                {
                    StopCoroutine(Talk());
                    StartCoroutine(Talk());
                }
            }
        }
    }

    void BuyandApply()
    {
        GameManager.instance.money -= cost;
        switch (itemCode)
        {
            case 10001:
                GameManager.instance.hp += GameManager.instance.maxHp * 3 / 10;
                if (GameManager.instance.hp > GameManager.instance.maxHp)
                {
                    GameManager.instance.hp = GameManager.instance.maxHp;
                }
                print("ü�� 30% ��� ȸ��");
                break;
            case 10002:
                GameManager.instance.hp += GameManager.instance.maxHp * 5 / 10;
                print("ü�� 50% ��� ȸ��");
                if (GameManager.instance.hp > GameManager.instance.maxHp)
                {
                    GameManager.instance.hp = GameManager.instance.maxHp;
                }
                break;
            case 10003:
                GameManager.instance.hp += GameManager.instance.maxHp * 7 / 10;
                print("ü�� 70% ��� ȸ��");
                if (GameManager.instance.hp > GameManager.instance.maxHp)
                {
                    GameManager.instance.hp = GameManager.instance.maxHp;
                }
                break;
            case 10004:
                GameManager.instance.hp = GameManager.instance.maxHp;
                print("ü�� 100% ��� ȸ��");
                break;
            case 20001:
                GameManager.instance.life++;
                print("��� �ϳ� �߰�");
                break;
        }
    }
}
