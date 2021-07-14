using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoroutineManager : MonoBehaviour
{
    public static CoroutineManager singleton;
    
    // Start is called before the first frame update
    void Start()
    {
        singleton = this;  
    }

    // Update is called once per frame
    public void Coroutine(IEnumerator cor)
    {
        StartCoroutine(cor);
    }
}

