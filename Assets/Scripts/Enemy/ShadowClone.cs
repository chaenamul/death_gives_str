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
        abilityName = "도플갱어";
        abilityText = "플레이어의 행동을 따라하는 그림자 생성";
    }

    protected override void Update()
    {

    }
    public override void GiveStr()
    {
        parent.GiveStr();
    }
}
