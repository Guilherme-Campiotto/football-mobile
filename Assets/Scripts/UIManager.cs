using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    public Text pointsUI;
    public Text ballUI;
    public static UIManager instance;
    private GameObject losePanel;
    private GameObject winPanel;
    private GameObject pausePanel;
    private Button pauseButton;
    private Button pauseButtonReturn;
    private Button menuButton;
    private Button restartLevelButton;
    private Button menuButtonWin;
    private Button restartLevelButtonWin;
    private Button nextLevelButton;
    public int coinsNumberBefore;
    public int coinsNumberAfter;
    public int coinsTotal;

    private void Awake()
    {

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        SceneManager.sceneLoaded += LoadUI;

        ChangePanelStatus();
        GetDataUI();
    }

    public void StartUIConfig()
    {
        ChangePanelStatus();
    }

    void LoadUI(Scene scene, LoadSceneMode mode)
    {
        GetDataUI();
    }

    void GetDataUI()
    {
        if (CurrentPosition.instance.level != 4)
        {
            pointsUI = GameObject.Find("Points").GetComponent<Text>();
            ballUI = GameObject.Find("BallsNumber").GetComponent<Text>();
            losePanel = GameObject.Find("Lose_panel");
            winPanel = GameObject.Find("Win_panel");
            pausePanel = GameObject.Find("Pause_panel");
            pauseButton = GameObject.Find("Button_pause").GetComponent<Button>();
            pauseButtonReturn = GameObject.Find("Button_pause_return").GetComponent<Button>();
            menuButton = GameObject.Find("Button_level_select_lose_panel").GetComponent<Button>();
            restartLevelButton = GameObject.Find("Button_play_again_lose_panel").GetComponent<Button>();

            menuButtonWin = GameObject.Find("Button_level_select_win_panel").GetComponent<Button>();
            restartLevelButtonWin = GameObject.Find("Button_play_again_win_panel").GetComponent<Button>();
            nextLevelButton = GameObject.Find("Button next_level").GetComponent<Button>();

            pauseButton.onClick.AddListener(PauseGame);
            pauseButtonReturn.onClick.AddListener(UnpauseGame);
            menuButton.onClick.AddListener(OpenLevelSelect);
            restartLevelButton.onClick.AddListener(RestartLevel);

            menuButtonWin.onClick.AddListener(OpenLevelSelect);
            restartLevelButtonWin.onClick.AddListener(RestartLevel);
            nextLevelButton.onClick.AddListener(NextLevel);

            coinsNumberAfter = PlayerPrefs.GetInt("coins");
        }
    }

    public void UpdateUI()
    {
        pointsUI.text = ScoreManager.instance.coins.ToString();
        ballUI.text = GameManager.instance.ballsNumber.ToString();
        coinsNumberAfter = ScoreManager.instance.coins;

    }

    public void GameOverUI()
    {
        losePanel.SetActive(true);
    }

    public void GameWinUI()
    {
        winPanel.SetActive(true);
    }

    public void PauseGame()
    {
        pausePanel.SetActive(true);
        pausePanel.GetComponent<Animator>().Play("ModeUIPause");
        Time.timeScale = 0;
    }

    public void UnpauseGame()
    {
        pausePanel.GetComponent<Animator>().Play("UIPauseReturn");
        Time.timeScale = 1;
        StartCoroutine(ClosePausePanel());
    }

    IEnumerator ClosePausePanel()
    {
        yield return new WaitForSeconds(0.5f);
        pausePanel.SetActive(false);
    }

    void ChangePanelStatus()
    {
        StartCoroutine(SetPanelsFalseIntimer());
    }

    IEnumerator SetPanelsFalseIntimer()
    {
        yield return new WaitForSeconds(0.001f);
        losePanel.SetActive(false);
        winPanel.SetActive(false);
        pausePanel.SetActive(false);
    }

    void RestartLevel()
    {
        if(!GameManager.instance.win)
        {
            SceneManager.LoadScene(CurrentPosition.instance.level);
            coinsTotal = coinsNumberAfter - coinsNumberBefore;
            ScoreManager.instance.LoseCoin(coinsTotal);
            coinsTotal = 0;
        } else
        {
            SceneManager.LoadScene(CurrentPosition.instance.level);
            coinsTotal = 0;
        }
    }

    void OpenLevelSelect()
    {
        if(!GameManager.instance.win)
        {
            coinsTotal = coinsNumberAfter - coinsNumberBefore;
            ScoreManager.instance.LoseCoin(coinsTotal);
            coinsTotal = 0;
            SceneManager.LoadScene(4);
        }
        else
        {
            coinsTotal = 0;
            SceneManager.LoadScene(4);
        }
    }

    void NextLevel()
    {
        if(GameManager.instance.win)
        {
            int level = CurrentPosition.instance.level + 1;
            SceneManager.LoadScene(level);
        }
    }

}
