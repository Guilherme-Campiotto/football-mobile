using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuStart : MonoBehaviour
{
    Animator configAnimator;
    Animator infoAnimator;
    bool isConfigOpen = false;
    Button buttonSound;

    public AudioSource audioManager;
    public Sprite audioOn, audioOff;


    private void Start()
    {
        configAnimator = GameObject.FindGameObjectWithTag("Config").GetComponent<Animator>();
        infoAnimator = GameObject.FindGameObjectWithTag("Info").GetComponent<Animator>();
        audioManager = GameObject.Find("AudioManager").GetComponent<AudioSource>();
        buttonSound = GameObject.Find("ButtonSound").GetComponent<Button>();
    }

    public void Play()
    {
        SceneManager.LoadScene(4);
    }

    public void OpenCloseConfiguration()
    {

        if(!isConfigOpen)
        {
            configAnimator.Play("MoveMenuAnimation");
            isConfigOpen = true;
        } else
        {
            configAnimator.Play("MoveMenuAnimationReverse");
            isConfigOpen = false;
        }

    }

    public void OpenInfo()
    {
        infoAnimator.Play("Info");
    }

    public void CloseInfo()
    {
        infoAnimator.Play("InfoReverse");
    }

    public void ChangeAudioState()
    {
        audioManager.mute = !audioManager.mute;

        if(audioManager.mute)
        {
            buttonSound.image.sprite = audioOff;
        } else
        {
            buttonSound.image.sprite = audioOn;
        }
    }

    public void OpenSocialMedia()
    {
        Application.OpenURL("www.facebook.com");
    }

}
