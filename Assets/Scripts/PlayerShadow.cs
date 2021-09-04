using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShadow : MonoBehaviour
{
    public static PlayerShadow instance { get; private set; }
    private Animator anim;
    public Attack attack { get; private set; }
    [NonSerialized]
    public Vector3 attackDir;
    [SerializeField]
    private HitBox hitbox;
    SpriteRenderer sprite;
    void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
        sprite = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
        transform.localPosition = new Vector3(0, 0, 0.1f);
        attack = new SwordAttack(GameManager.instance.Dmg / 5, 0.25f, hitbox, gameObject, GameManager.instance.playerController);
    }

    void Update()
    {
        attack.DelayUpdate();
        float dx = Input.GetAxisRaw("Horizontal");
        if (dx > 0)
        {
            transform.localPosition = new Vector3(-1, 0, 0.1f);
            sprite.flipX = true;
        }
        else if(dx<0)
        {
            transform.localPosition = new Vector3(1, 0, 0.1f);
            sprite.flipX = false;
        }
        if (Input.GetMouseButtonDown(0) && attack.curNormalDelay<=0)
        {
            Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            mousepos.z -= Camera.main.transform.position.z;
            attackDir = (mousepos - GameManager.instance.playerController.transform.position).normalized * 2.5f;
            Invoke("AttackRoutine", 0.1f);
        }
    }
    void AttackRoutine()
    {
        sprite.enabled = true;
        AttackAnimation();
        attack.Execute();
        Invoke("ShadowDisable", 0.15f);
    }
    void ShadowDisable()
    {
        sprite.enabled = false;
    }
    void AttackAnimation()
    {
        StartCoroutine(attackAnimation());
    }
    IEnumerator attackAnimation()
    {
        if(anim)
            anim.SetBool("isAttacking", true);
        yield return new WaitForSeconds(0.2f);
        if(anim)
            anim.SetBool("isAttacking", false);
    }

}
