using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Door : MonoBehaviour
{
    public string curScene;
    public string sceneToLoad;

    void OnTriggerStay2D(Collider2D collision)
    {
        print("�ȳ�");
        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.W))
        {
            print("�ȳ�2");
            SaveManager.instance.pushData(curScene);
            SceneManager.LoadScene(sceneToLoad);
        }
    }
}
