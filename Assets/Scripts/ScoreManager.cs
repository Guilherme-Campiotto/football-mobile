using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreManager : MonoBehaviour
{
    public static ScoreManager instance;
    public int coins;

    private void Awake()
    {

        if(instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else
        {
            Destroy(gameObject);
        }
    }

    public void GameStartScore()
    {
        if(PlayerPrefs.HasKey("coinsSaved"))
        {
            coins = PlayerPrefs.GetInt("coinsSaved");
        } else
        {
            coins = 100;
            PlayerPrefs.SetInt("coinsSaved", coins);
        }
    }

    public void UpdateScore()
    {
        coins = PlayerPrefs.GetInt("coinsSaved");
    }

    public void CollectCoins(int coinsCollected)
    {
        coins += coinsCollected;
        SaveCoins(coins);
    }

    public void LoseCoin(int coinsLost)
    {
        coins -= coinsLost;
        SaveCoins(coins);
    }

    public void SaveCoins(int coinsCollected)
    {
        PlayerPrefs.SetInt("coinsSaved", coinsCollected);
    }
}
