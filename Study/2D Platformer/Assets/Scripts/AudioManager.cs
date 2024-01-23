using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;
    
    void Awake()
    {
        if (instance == null)
        {
            //instance = this;
            //DontDestroyOnLoad(gameObject);
            SetupAudioManager();
        } 
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void SetupAudioManager()
    {
        instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public AudioSource menuMusic, bossMusic, levelCompleteMusic;
    public AudioSource[] levelTracks;
    
    public AudioSource[] allSFX;
    
    [Range(0f, 1f)]
    public float currentVolume;

    void Start()
    {
        SetAllVolumes();
    }

    void Update()
    {
#if UNITY_EDITOR
        if ( Input.GetKeyDown(KeyCode.M) )
        {
            PlayLevelMusic(0);
        }
#endif
    }

    void SetAllVolumes()
    {
        menuMusic.volume = currentVolume;
        bossMusic.volume = currentVolume;
        levelCompleteMusic.volume = currentVolume;

        foreach ( var track in levelTracks )
        {
            track.volume = currentVolume;
        }
    }

    void StopMusic()
    {
        menuMusic.Stop();
        bossMusic.Stop();
        levelCompleteMusic.Stop();

        foreach ( var track in levelTracks )
        {
            track.Stop();
        }
    }

    public void PlayMenuMusic()
    {
        StopMusic();
        menuMusic.Play();
    }

    public void PlayBossMusic()
    {
        StopMusic();
        bossMusic.Play();
    }

    public void PlayLevelCompleteMusic()
    {
        StopMusic();
        levelCompleteMusic.Play();
    }


    public void PlayLevelMusic( int trackToPlay )
    {
        StopMusic();
        levelTracks[trackToPlay].Play();
    }

    public void PlaySFX( int sfxToPlay )
    {
        allSFX[sfxToPlay].Stop();
        allSFX[sfxToPlay].Play();
    }

    public void PlaySFXPitched( int sfxToPlay )
    {
        allSFX[sfxToPlay].Stop();

        allSFX[sfxToPlay].pitch = Random.Range(.75f, 1.25f);
        
        allSFX[sfxToPlay].Play();
    }
}
