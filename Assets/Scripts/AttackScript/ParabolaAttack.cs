using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;

public class ParabolaAttack : Attack
{
    public float xSpeed;
    Rigidbody2D rbody;
    bool ground;
    public override Vector3 TargetUpdate()
    {
        Vector3 attackDir = new Vector3(0, 0, 0);
        if (subject.tag == "Player")
        {

        }
        else
        {
            if (ground)
            {
                RaycastHit2D hit = Physics2D.Raycast(GameManager.instance.playerController.transform.position + new Vector3(0,0,1), new Vector3(0,-1,0), Mathf.Infinity, ~(1<<GameManager.instance.playerController.transform.gameObject.layer | 1<<subject.layer));
                Debug.DrawRay(GameManager.instance.playerController.transform.position + new Vector3(0, 0, 1), new Vector3(0, -1, 0) * hit.distance, Color.red, 1.0f);
                Debug.Log(hit.distance);
                attackDir = GameManager.instance.playerController.transform.position - new Vector3(0, hit.distance, 0);
            }
            else
            {
                attackDir = (GameManager.instance.playerController.transform.position);
            }
        }
        return attackDir;
    }

    public override IEnumerator Normal()
    {
        hb.transform.position = subject.transform.position; 
        hb.gameObject.SetActive(true);
        Vector3 target = TargetUpdate();
        float time = (target.x - subject.transform.position.x) / xSpeed;
        if (time < 0)
        {
            xSpeed *= -1;
            time *= -1;
        }
        float ySpeed = -rbody.gravityScale * Physics2D.gravity.y / 2.0f * time + (target.y - subject.transform.position.y) / time;
        rbody.velocity = new Vector3(xSpeed, ySpeed, 0);
        Debug.Log(rbody.velocity);
        yield return null;
    }
    public ParabolaAttack(int dmg, float delay, HitBox hitbox, GameObject sub, object component, float xSpeed, bool ground) : base(dmg, delay, hitbox, sub, component)
    {
        this.xSpeed = xSpeed;
        this.ground = ground;
        rbody = hitbox.GetComponent<Rigidbody2D>();
        
    }
}
