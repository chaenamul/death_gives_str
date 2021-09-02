using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DoubleCoin : Item
{
    [SerializeField]
    private Text fullText;

    protected override void Update()
    {
        base.Update();

        if (isColliding)
        {
            if (Input.GetKeyDown(KeyCode.Q))
            {
                if (GameManager.instance.money >= cost)
                {
                    Buy();
                }
                else
                {
                    StopCoroutine(Talk());
                    StartCoroutine(Talk());
                }
            }
        }
    }

    void Buy()
    {
        if (items.Count != 0)
        {
            StopCoroutine(Full());
            StartCoroutine(Full());
        }
        else
        {
            GameManager.instance.money -= cost;
            items.Add(itemCode);
        }
    }

    IEnumerator Full()
    {
        fullText.gameObject.SetActive(true);
        yield return new WaitForSeconds(2f);
        fullText.gameObject.SetActive(false);
    }
}