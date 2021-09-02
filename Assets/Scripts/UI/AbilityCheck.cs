using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AbilityCheck : MonoBehaviour
{
    private static AbilityCheck instance = null;

    public Text abilityText;

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
    }

    public void abilityNameCheck(string Wability)
    {
        abilityText.text = "목숨을 대가로 능력 \"" + GameManager.instance.abilities[GameManager.instance.playerController.Getability] + "\" 를 얻었습니다\n\n" + Wability;
    }
}
