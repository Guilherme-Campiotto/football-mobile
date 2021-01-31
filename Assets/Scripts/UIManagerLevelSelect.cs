using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerLevelSelect : MonoBehaviour
{
    public Text coins;

    // Start is called before the first frame update
    void Start()
    {
        coins.text = PlayerPrefs.GetInt("coinsSaved").ToString();
        ScoreManager.instance.UpdateScore();
    }

}
