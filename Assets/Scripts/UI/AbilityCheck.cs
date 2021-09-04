using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCheck : MonoBehaviour
{
    private static AbilityCheck instance = null;

    public Text abilityText;
    public Image mobimage;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        gameObject.SetActive(false);
    }

    public void resume()
    {
        gameObject.SetActive(false);
        Time.timeScale = 1;
        GameManager.instance.playerController.Getability = null;
    }

    public void abilityNameCheck(string Wability)
    {
        abilityText.text = "<" + GameManager.instance.playerController.Getability + ">\n\n" + Wability;
        mobimage.sprite = GameManager.instance.playerController.MonsterImage;
    }
}
