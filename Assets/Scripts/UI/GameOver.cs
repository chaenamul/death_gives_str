using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOver : MonoBehaviour
{
    private static GameOver instance = null;

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
    public void BackToMainMenu()
    {
        GameManager.instance.Initialize();
        SaveManager.instance.Initialize();
        GameManager.instance.gameOverPanel.SetActive(false);
        GameManager.instance.playerController.gameObject.SetActive(true);
        SaveManager.instance.BackToMainMenu();
    }
}
