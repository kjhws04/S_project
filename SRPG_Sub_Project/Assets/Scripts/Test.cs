using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(test());
    }

    IEnumerator test()
    {
        for (int i = 0; i < 10; i++)
        {
            Managers.Combat.Init();
            new WaitForSeconds(0.1f);
        }
        yield return null;
    }

    void Update()
    {
        
    }
}
