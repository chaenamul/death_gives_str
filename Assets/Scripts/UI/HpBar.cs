using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HpBar : MonoBehaviour
{
    [SerializeField]
    private Image hpBar;
    
    void Update()
    {
        if (!GameManager.instance.playerController.isGhost)
            hpBar.fillAmount = (float)GameManager.instance.hp / (float)GameManager.instance.maxHp;
        else
            hpBar.fillAmount = (float)GameManager.instance.hp / (float)GameManager.instance.ghostHp;
    }
}
