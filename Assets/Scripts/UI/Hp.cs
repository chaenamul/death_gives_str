using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    public Text hpText;

    void Update()
    {
        if(!GameManager.instance.playerController.isGhost)
            hpText.text = string.Format("HP {0}/{1}", GameManager.instance.hp, GameManager.instance.maxHp);
        else
            hpText.text = string.Format("HP {0}/{1}", GameManager.instance.hp, GameManager.instance.ghostHp);
    }
}
