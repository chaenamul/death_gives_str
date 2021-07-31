using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zombie : Enemy
{


    void Die()
    {
        gameObject.SetActive(false);
    }
}
