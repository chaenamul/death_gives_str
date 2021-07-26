using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    public HitBox hitBox;
    public PlayerController playerController;
    public Skeleton skeleton;

    void Awake()
    {
        instance = this;
    }
}
