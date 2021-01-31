using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KillEffect : MonoBehaviour
{
    private void Start()
    {
        StartCoroutine(KillObject());
    }
    IEnumerator KillObject()
    {
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
