using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuTransition : MonoBehaviour
{
    public void OpenStore()
    {
        SceneManager.LoadScene(5);
    }

    public void OpenLevelSelect()
    {
        SceneManager.LoadScene(4);
    }
}
