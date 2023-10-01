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
            Debug.Log("Paused");
            isMusicPlaying = focus;
        }
        else
        {
            Debug.Log("Resume");
            isMusicPlaying = focus;
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
        }
    }

    AudioSO GetRandomSong()
    {
        return allSongs[Random.Range(0, allSongs.Length)];
    }

    private void FixedUpdate()
    {
        if (currentMusicTime <= currentSong.GetClipLength() && isMusicPlaying)
        {
            currentMusicTime += Time.deltaTime;
        }
        else
        {
            currentMusicTime = 0;

            currentSong = GetRandomSong();

            audioSource.clip = currentSong.GetAudioClip();
            audioSource.volume = currentSong.GetVolume();

            audioSource.Play();
        }
    }
}
