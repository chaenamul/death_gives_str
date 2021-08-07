using System.Collections;
using System.Collections.Generic;
using Unity.IO.LowLevel.Unsafe;
using UnityEngine;

public class HitBox : MonoBehaviour
{
    public int dmg;
    public object subject;
    protected virtual void OnTriggerEnter2D(Collider2D collision)
    {
        print(subject as PlayerController);
        print("collision: " + collision.gameObject);


        if(subject as Enemy && collision.tag == "Player") // Enemy -> Player АјАн
        {
            GameManager.instance.playerController.GetDmg((Enemy)subject, dmg);
        }
        else if(subject as PlayerController && collision.tag.Contains("Enemy"))
        {
            InvokeRepeating("Shaking", 0f, 0.005f);
            Invoke("StopShaking", 0.2f);
            collision.GetComponent<Enemy>().GetDmg(dmg);
        }
    }
    void Shaking()
    {
        Camera.main.transform.position = Random.insideUnitSphere * 0.05f + new Vector3(0f, 0f, -10f);
    }
    void StopShaking()
    {
        CancelInvoke("Shaking");
        Camera.main.transform.position = new Vector3(0f, 0f, -10f);
    }
}
