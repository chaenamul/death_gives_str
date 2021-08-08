using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string curScene;
    public string sceneToLoad;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.W))
        {
            SaveManager.instance.MoveToNextScene(curScene, sceneToLoad);
        }
    }

    /// <summary>
    /// 개발자도구
    /// 9 == > 이전씬
    /// 0 == > 다음씬
    /// Door가 있는 Scene에서만 작동함
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
