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
        hpBar.fillAmount = (float)GameManager.instance.hp / (float)GameManager.instance.maxHp;
    }
}
