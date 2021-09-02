using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Item : MonoBehaviour
{
    protected List<int> items = new List<int>();

    [SerializeField]
    protected int itemCode;
    [SerializeField]
    protected int cost;
    [SerializeField]
    private GameObject costTextPrefab;
    [SerializeField]
    protected GameObject canvas;
    [SerializeField]
    private Text noticeText;
    [SerializeField]
    private GameObject infoPrefab;
    [SerializeField]
    private string infoText;

    protected bool isColliding = false;
    private Text costText;
    private GameObject info;

    void Start()
    {
        costText = Instantiate(costTextPrefab, canvas.transform).GetComponent<Text>();
        costText.text = cost.ToString();
    }

    protected virtual void Update()
    {
        Vector3 costTextPos = Camera.main.WorldToScreenPoint(new Vector3(transform.position.x, transform.position.y - 2f, 0));
        costText.transform.position = costTextPos;
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

    protected IEnumerator Talk()
    {
        noticeText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        noticeText.gameObject.SetActive(false);
    }
}
