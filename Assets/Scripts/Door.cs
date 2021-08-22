using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string curScene;
    public string sceneToLoad;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (GameManager.instance.monsterCount == 0 && collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.W))
        {
            SaveManager.instance.MoveToNextScene(curScene, sceneToLoad);
        }
        else if(collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.W))
        {
            Debug.Log("Door is closed");
        }
    }

    /// <summary>
    /// �����ڵ���
    /// 9 == > ������
    /// 0 == > ������
    /// Door�� �ִ� Scene������ �۵���
    /// </summary>
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha9))
        {
            SaveManager.instance.MoveToPrevScene();
        }
        if (Input.GetKeyDown(KeyCode.Alpha0))
        {
            SaveManager.instance.MoveToNextScene(curScene, sceneToLoad);
        }
    }
}
