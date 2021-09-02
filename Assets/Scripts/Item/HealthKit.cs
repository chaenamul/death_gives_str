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
                print("체력 30% 즉시 회복");
                break;
            case 10002:
                GameManager.instance.hp += GameManager.instance.maxHp * 5 / 10;
                print("체력 50% 즉시 회복");
                if (GameManager.instance.hp > GameManager.instance.maxHp)
                {
                    GameManager.instance.hp = GameManager.instance.maxHp;
                }
                break;
            case 10003:
                GameManager.instance.hp += GameManager.instance.maxHp * 7 / 10;
                print("체력 70% 즉시 회복");
                if (GameManager.instance.hp > GameManager.instance.maxHp)
                {
                    GameManager.instance.hp = GameManager.instance.maxHp;
                }
                break;
            case 10004:
                GameManager.instance.hp = GameManager.instance.maxHp;
                print("체력 100% 즉시 회복");
                break;
            case 20001:
                GameManager.instance.life++;
                print("목숨 하나 추가");
                break;
        }
    }
}
