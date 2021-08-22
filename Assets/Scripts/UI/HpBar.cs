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
        hpBar.fillAmount = GameManager.instance.hp / 100f;
    }
}
