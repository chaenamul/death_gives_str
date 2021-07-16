using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Skeleton : MonoBehaviour
{
    // Mob Settings
    [SerializeField]
    private float hp;
    [SerializeField]
    private float dmg;
    [SerializeField]
    private float speed;
    [SerializeField]
    private float sight;

    private bool isAggressive;

    void Start()
    {
        isAggressive = false;
    }

    void Update()
    {
        FindPlayer();
        Attack();
    }

    void FindPlayer()
    {
        if (Vector3.Distance(GameManager.instance.playerController.transform.position, transform.position) <= sight)
        {
            isAggressive = true;
        }
    }

    void Attack()
    {
        if (isAggressive)
        {
            // ÀÌ½ÂÀ±
        }
    }
}
