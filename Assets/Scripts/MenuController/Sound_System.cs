using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sound_System: MonoBehaviour
{
    public static Sound_System instance;
    public AudioClip[] themeSongs;
    public AudioSource soundFX, audioTheme, Voice;

    void Awake()
    {
        instance = this;

    }

    void Start()
    {


        if (!audioTheme.playOnAwake)
        {
            audioTheme.clip = themeSongs[Random.Range(0, themeSongs.Length)];
            audioTheme.Play();
        }
    }
    void Update()
    {
        if (!audioTheme.isPlaying)
        {
            audioTheme.clip = themeSongs[Random.Range(0, themeSongs.Length)];
            audioTheme.Play();
        }
    }

    public void PlaySoundFX(AudioClip clip)
    {
        soundFX.clip = clip;
        soundFX.Play();
    }
    public void Voices(AudioClip clip)
    {
        Voice.clip = clip;
        Voice.Play();
    }

}
