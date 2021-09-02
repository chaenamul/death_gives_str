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
        abilityText.text = "����� �밡�� �ɷ� \"" + GameManager.instance.abilities[GameManager.instance.playerController.Getability] + "\" �� ������ϴ�\n\n" + Wability;
    }
}
