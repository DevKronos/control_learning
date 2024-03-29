using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FX : MonoBehaviour
{
    void Start()
    {
        StartCoroutine(Brake());
    }

    IEnumerator Brake()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }

}
