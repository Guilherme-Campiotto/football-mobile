using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    [SerializeField]
    private GameObject[] balls;
    public int ballsNumber = 2;
    private bool ballDead = false;
    public int ballInsideScene = 0;
    private Transform transform;
    public bool win;
    public int shoot = 0;
    //public int currentScene;
    public bool gameStarted;

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

        SceneManager.sceneLoaded += LoadPosition;

        transform = GameObject.Find("InicialPosition").GetComponent<Transform>();
    }

    void Start()
    {
        ScoreManager.instance.GameStartScore();
        StartGame();
    }

    void Update()
    {
        ScoreManager.instance.UpdateScore();
        UIManager.instance.UpdateUI();
        RespawnBall();

        if(ballsNumber <= 0 && gameStarted)
        {
            GameOver();
        }

        if(win)
        {
            GameWin();
        }
    }

    void RespawnBall()
    {
        if(ballsNumber > 0 && ballInsideScene == 0)
        {
            Instantiate(balls[CurrentPosition.instance.ballSelected], new Vector2(transform.position.x, transform.position.y), Quaternion.identity);
            ballInsideScene += 1;
            shoot = 0;
        }
    }

    void LoadPosition(Scene scene, LoadSceneMode mode)
    {
        if(CurrentPosition.instance.level != 4)
        {
            transform = GameObject.Find("InicialPosition").GetComponent<Transform>();
            StartGame();
        }
    }

    void GameOver()
    {
        UIManager.instance.GameOverUI();
        gameStarted = false;
    }

    void GameWin() {
        UIManager.instance.GameWinUI();
        gameStarted = true;
    }

    void StartGame()
    {
        gameStarted = true;
        ballsNumber = 2;
        ballInsideScene = 0;
        win = false;
        UIManager.instance.StartUIConfig();
    }
}
