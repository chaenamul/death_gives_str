using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;
using UnityEngine.UI;

public class HitBox : MonoBehaviour
{
    public int dmg;
    public object subject;
    private int countTime = 0;
    private bool inv = false;

    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        print(subject as PlayerController);
        print("collision: " + collision.gameObject);

        if (subject as Enemy && collision.tag == "Player" && !inv) // Enemy -> Player АјАн
        {
            GameManager.instance.playerController.GetDmg((Enemy)subject, dmg);
            inv = true;
            Invoke("ShowHitimage", 0f);
            Invoke("StopHitimage", 0.1f);
            InvokeRepeating("Light", 0f, 0.2f);
            Invoke("StopLight", 3f);
        }
        else if (subject as PlayerController && collision.tag.Contains("Enemy"))
        {
            InvokeRepeating("Shaking", 0f, 0.005f);
            Invoke("StopShaking", 0.2f);
            collision.GetComponent<Enemy>().GetDmg(dmg);
        }
    }

    void Shaking()
    {
        Camera.main.transform.position = Random.insideUnitSphere * 0.05f + new Vector3(GameManager.instance.playerController.transform.position.x, 0f, -10f);
    }

    void StopShaking()
    {
        CancelInvoke("Shaking");
        Camera.main.transform.position = new Vector3(GameManager.instance.playerController.transform.position.x, 0f, -10f);
    }

    void Light()
    {
        if (countTime % 2 == 0)
        {
            GameObject.Find("Player").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 90);
        }
        else
        {
            GameObject.Find("Player").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 180);
        }
        countTime++;
    }

    void StopLight()
    {
        CancelInvoke("Light");
        GameObject.Find("Player").GetComponent<SpriteRenderer>().color = new Color32(255, 255, 255, 255);
        countTime = 0;
        inv = false;
    }

    void ShowHitimage()
    {
        GameObject.Find("Hitimage").GetComponent<Image>().color = new Color(1, 0, 0, 0.2f);
    }

    void StopHitimage()
    {
        GameObject.Find("Hitimage").GetComponent<Image>().color = new Color(1, 0, 0, 0);
    }
}