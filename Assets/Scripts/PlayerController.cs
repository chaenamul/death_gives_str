using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;
    public float attackDelay;
    public float attackDamage;

    private float delay = 0;

    void Awake()
    {
    }

    void Update()
    {
        Move();
        delay -= Time.deltaTime;
        if(Input.GetMouseButtonDown(0) && delay <= 0)
        {
            delay = attackDelay;
            StartCoroutine(Attack());
        }
    }

    void Move()
    {
        float dirx = Input.GetAxisRaw("Horizontal");
        float diry = Input.GetAxisRaw("Vertical");
        transform.Translate(new Vector2(dirx, diry) * speed * Time.deltaTime);
    }

    IEnumerator Attack()
    {
        Vector3 mousepos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        mousepos.z -= Camera.main.transform.position.z;
        Vector3 attackDir = mousepos - transform.position;
        attackDir = attackDir.normalized;
        GameManager.instance.hitBox.transform.position = transform.position + attackDir;
        GameManager.instance.hitBox.gameObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        GameManager.instance.hitBox.gameObject.SetActive(false);
    }
}
