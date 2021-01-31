using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] clips;
    public AudioSource musicBackground;

    public AudioClip[] clipsFX;
    public AudioSource soundsFX;

    public static AudioManager instance;

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
    }

    private void Update()
    {
        if(!musicBackground.isPlaying)
        {
            musicBackground.clip = getMusicRandom();
            musicBackground.Play();
        }
    }

    public AudioClip getMusicRandom()
    {
        return clips[Random.Range(0, clips.Length)];
    }

    public void PlaySoundEffect(int index)
    {
        soundsFX.clip = clipsFX[index];
        soundsFX.Play();
    }
}
