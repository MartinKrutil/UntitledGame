using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager instance;

    private AudioSource soundSource;

    private void Awake()
    {
        if (instance != null && instance != this)
            Destroy(this);

        else
            instance = this;

        soundSource = GetComponentInChildren<AudioSource>();
    }

    public void PlaySound(AudioClip audioClip) => soundSource.PlayOneShot(audioClip);   
}
