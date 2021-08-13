using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private GameObject target;

    public Vector3 CameraShake;

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
            transform.position = new Vector3(target.transform.position.x, transform.position.y, transform.position.z);
            CameraShake = transform.position;
        }
    }
}
