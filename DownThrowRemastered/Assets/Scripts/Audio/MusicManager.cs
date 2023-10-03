using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    AudioSO[] allSongs;

    AudioSO currentSong;
    float currentMusicTime = 0;
    bool isMusicPlaying = false;
    const int EXTRA_SONG_TIME = 1;

    public static MusicManager Instance;

    private void OnApplicationFocus(bool focus)
    {
        if (!focus)
        {
            PauseSong();
        }
        else
        {
            ResumeSong();
        }
    }

    private void Start()
    {
        Init();
    }

    void Init()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            allSongs = Resources.LoadAll<AudioSO>("Music");
            NewSong();
            ResumeSong();
        }
    }

    AudioSO GetRandomSong()
    {
        return allSongs[Random.Range(0, allSongs.Length)];
    }

    void PauseSong()
    {
        audioSource.Stop();
        isMusicPlaying = false;
    }

    void ResumeSong()
    {
        audioSource.Play();
        audioSource.time = currentMusicTime;
        isMusicPlaying = true;
    }

    void NewSong()
    {
        currentSong = GetRandomSong();
        audioSource.clip = currentSong.GetAudioClip();
        audioSource.volume = currentSong.GetVolume();

        currentMusicTime = 0;
    }

    private void FixedUpdate()
    {
        if (!isMusicPlaying) return;

        if (currentMusicTime <= currentSong.GetClipLength())
        {
            currentMusicTime += Time.deltaTime;
        }
        else
        {
            NewSong();
        }
    }
}
