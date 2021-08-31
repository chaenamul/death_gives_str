using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public Text moneyText;

    void Update()
    {
        moneyText.text = GameManager.instance.money.ToString();
    }
}
