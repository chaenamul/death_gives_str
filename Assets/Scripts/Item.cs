using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    [SerializeField]
    private int itemCode;
    [SerializeField]
    private int cost;
    [SerializeField]
    private GameObject costTextPrefab;
    [SerializeField]
    private GameObject canvas;
    [SerializeField]
    private Text noticeText;
    [SerializeField]
    private GameObject infoPrefab;
    [SerializeField]
    private string infoText;

    public bool isColliding = false;
    private Text costText;
    private GameObject info;

    void Start()
    {
        costText = Instantiate(costTextPrefab, canvas.transform).GetComponent<Text>();
        costText.text = cost.ToString();
    }

    void Update()
    {
        Vector3 costTextPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - 1.3f, 0));
        costText.transform.position = costTextPos;

        if (isColliding)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (GameManager.instance.money >= cost)
                {
                    BuyandApply();
                }
                else
                {
                    StopCoroutine(Talk());
                    StartCoroutine(Talk());
                }
            }
        }
    }

    void BuyandApply()
    {
        GameManager.instance.money -= cost;
        switch (itemCode)
        {
            case 10001:
                GameManager.instance.hp += GameManager.instance.maxHp * 3 / 10;
                if (GameManager.instance.hp > GameManager.instance.maxHp)
                {
                    GameManager.instance.hp = GameManager.instance.maxHp;
                }
                print("체력 30% 즉시 회복");
                break;
            case 10002:
                GameManager.instance.hp += GameManager.instance.maxHp * 5 / 10;
                print("체력 50% 즉시 회복");
                if (GameManager.instance.hp > GameManager.instance.maxHp)
                {
                    GameManager.instance.hp = GameManager.instance.maxHp;
                }
                break;
            case 10003:
                GameManager.instance.hp += GameManager.instance.maxHp * 7 / 10;
                print("체력 70% 즉시 회복");
                if (GameManager.instance.hp > GameManager.instance.maxHp)
                {
                    GameManager.instance.hp = GameManager.instance.maxHp;
                }
                break;
            case 10004:
                GameManager.instance.hp = GameManager.instance.maxHp;
                print("체력 100% 즉시 회복");
                break;
            case 20001:
                GameManager.instance.life++;
                print("목숨 하나 추가");
                break;
        }
    }

    IEnumerator Talk()
    {
        noticeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        noticeText.gameObject.SetActive(false);
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            info = Instantiate(infoPrefab, canvas.transform);
            info.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 3f);
            info.transform.GetChild(0).GetComponent<Text>().text = infoText;
        }
    }

    void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isColliding = true;
            info.transform.position = Camera.main.WorldToScreenPoint(transform.position + Vector3.up * 3f);
        }
    }

    void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            isColliding = false;
            Destroy(info);
        }
    }
}
