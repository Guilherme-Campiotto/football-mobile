using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CurrentPosition : MonoBehaviour
{
    public int level = -1;
    public static CurrentPosition instance;
    [SerializeField]
    private GameObject UIManager;
    [SerializeField]
    private GameObject GameManager;
    public int ballSelected;

    [SerializeField]
    public float orthoSize = 5;
    [SerializeField]
    private float aspect = 1.75f;

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

        SceneManager.sceneLoaded += CheckLevel;

        ballSelected = PlayerPrefs.GetInt("BallSelected");
    }

    void CheckLevel(Scene scene, LoadSceneMode mode)
    {
        level = SceneManager.GetActiveScene().buildIndex;

        if(level != 4 && level != 5 && level != 6)
        {
            Instantiate(UIManager);
            Instantiate(GameManager);
            Camera.main.projectionMatrix = Matrix4x4.Ortho(-orthoSize * aspect, orthoSize * aspect, -orthoSize, orthoSize, Camera.main.nearClipPlane, Camera.main.farClipPlane);
        }
    }

}
