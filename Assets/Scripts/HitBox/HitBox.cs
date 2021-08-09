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

        if (subject as Enemy && collision.tag == "Player" && !GameManager.instance.playerController.inv) // Enemy -> Player АјАн
        {
            GameManager.instance.playerController.GetDmg((Enemy)subject, dmg);
        }
        else if (subject as PlayerController && collision.tag.Contains("Enemy"))
        {
            collision.GetComponent<Enemy>().GetDmg(dmg);
        }
    }

}