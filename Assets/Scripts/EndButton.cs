using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndButton : MonoBehaviour
{
    private bool isColliding = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isColliding)
        {
            GameManager.instance.hp = GameManager.instance.maxHp;
            GameManager.instance.life = 9;
            SaveManager.instance.clearList.Clear();
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isColliding = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isColliding = false;
        }
    }
}
