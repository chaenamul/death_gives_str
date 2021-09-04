using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShadowClone : Enemy
{
    [SerializeField]
    private Shadow parent;
    protected override void Awake()
    {

    }

    protected override void Start()
    {
        
    }

    protected override void Update()
    {

    }
    public override void GiveStr()
    {
        parent.GiveStr();
    }
}
