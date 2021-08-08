using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Life : MonoBehaviour
{
    public Text lifeText;

    void Update()
    {
        lifeText.text = GameManager.instance.life.ToString();
    }
}
