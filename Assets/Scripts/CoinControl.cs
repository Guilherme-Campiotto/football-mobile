using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinControl : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Ball"))
        {
            AudioManager.instance.PlaySoundEffect(0);
            ScoreManager.instance.CollectCoins(10);
            Destroy(gameObject);
        }
    }
}
