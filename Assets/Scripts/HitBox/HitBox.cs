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


        if(subject as Enemy && collision.tag == "Player") // Enemy -> Player 공격
        {
            GameManager.instance.playerController.GetDmg((Enemy)subject, dmg);
        }
        else if(subject as PlayerController && collision.tag.Contains("Enemy"))
        {
            ///여기 화면 흔들림 구현
            ///
            ///
            ///
            ///
            collision.GetComponent<Enemy>().GetDmg(dmg);
        }
    }
}
