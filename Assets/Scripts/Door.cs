using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : MonoBehaviour
{
    public string curScene;
    public string sceneToLoad;
    private BoxCollider2D bc;
    private SpriteRenderer sprite;

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.W))
        {
            SaveManager.instance.MoveToNextScene(curScene, sceneToLoad);
        }
        else if(collision.gameObject.tag == "Player" && Input.GetKey(KeyCode.W))
        {
            Debug.Log("Door is closed");
        }
    }
    private void Awake()
    {
        bc = transform.GetComponent<BoxCollider2D>();
        sprite = transform.GetComponent<SpriteRenderer>();
        bc.enabled = false;
        sprite.enabled = false;
    }

    /// <summary>
    /// �����ڵ���
    /// 9 == > ������
    /// 0 == > ������
    /// Door�� �ִ� Scene������ �۵���
    /// </summary>
    private void Update()
    {
        if(GameManager.instance.monsterCount == 0)
        {
            bc.enabled = true;
            sprite.enabled = true;
        }
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
