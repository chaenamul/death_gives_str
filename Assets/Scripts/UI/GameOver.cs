using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameOver : MonoBehaviour
{
    void Awake()
    {
        var obj = FindObjectsOfType<GameOver>();
        if (obj.Length == 1)
        {
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
        SaveManager.instance.BackToMainMenu();
    }
}
