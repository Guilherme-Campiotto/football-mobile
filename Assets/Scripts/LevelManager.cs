using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [System.Serializable]
    public class Level
    {
        public string levelText;
        public bool levelAvailable;
        public int unlocked;
        public bool showText;
    }

    public GameObject button;
    public Transform buttomPosition;
    public List<Level> levelList;


    void Start()
    {
        AddList();
    }

    private void Awake()
    {
        Destroy(GameObject.Find("UIManager(Clone)"));
        Destroy(GameObject.Find("GameManager(Clone)"));
    }

    void AddList()
    {
        foreach (Level level in levelList)
        {
            GameObject newButton = Instantiate(button) as GameObject;
            newButton.transform.SetParent(buttomPosition, false);

            ButtomLevel btnLevel = newButton.GetComponent<ButtomLevel>();
            btnLevel.levelTextButton.text = level.levelText;

            if(PlayerPrefs.GetInt("Level" + btnLevel.levelTextButton.text) == 1)
            {
                level.unlocked = 1;
                level.levelAvailable = true;
                level.showText = true;
            }

            btnLevel.GetComponentInChildren<Text>().enabled = level.showText;
            btnLevel.unlockedButton = level.unlocked;
            btnLevel.GetComponent<Button>().interactable = level.levelAvailable;
            btnLevel.GetComponent<Button>().onClick.AddListener(() => ClickLevel("Level" + btnLevel.levelTextButton.text));
        }
    }

    void ClickLevel(string level)
    {
        SceneManager.LoadScene(level);
    }
}
