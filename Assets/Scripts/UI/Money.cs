using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Money : MonoBehaviour
{
    public Text moneyText;

    void Update()
    {
        moneyText.text = "Money: " + GameManager.instance.money;
    }
}
