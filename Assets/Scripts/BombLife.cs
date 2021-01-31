using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombLife : MonoBehaviour
{
    private GameObject barrel;

    // Start is called before the first frame update
    void Start()
    {
        barrel = GameObject.Find("BarrelTNT");
    }

    // Update is called once per frame
    void Update()
    {
        StartCoroutine(AutoDestroy());
    }

    IEnumerator AutoDestroy()
    {
        Destroy(barrel);
        yield return new WaitForSeconds(0.5f);
        Destroy(gameObject);
    }
}
