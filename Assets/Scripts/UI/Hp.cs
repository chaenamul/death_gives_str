using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Hp : MonoBehaviour
{
    public Text hpText;

    void Update()
    {
        hpText.text = string.Format("HP {0}/100", GameManager.instance.hp);
    }
}
