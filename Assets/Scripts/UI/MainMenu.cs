using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private string sceneToLoad;

    void Update()
    {
        StartGame();
    }

    void StartGame()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GameManager.instance.playerController.gameObject.SetActive(true);
            SaveManager.instance.MoveToNextScene(null, sceneToLoad);
        }
    }
}
