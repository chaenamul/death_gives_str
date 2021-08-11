using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject target;
    [SerializeField]
    private float speed;

    private Vector3 targetPos;

    void Awake()
    {
        var obj = FindObjectsOfType<CameraController>();
        if (obj.Length == 1)
        {
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Update()
    {
        if (target.gameObject != null)
        {
            targetPos.Set(target.transform.position.x, transform.position.y, transform.position.z);
            this.transform.position = Vector3.Lerp(transform.position, targetPos, speed * Time.deltaTime);
        }
    }
}
