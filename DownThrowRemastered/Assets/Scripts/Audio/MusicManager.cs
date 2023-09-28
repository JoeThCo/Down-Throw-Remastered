using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MusicManager : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;
    AudioSO[] allSongs;

    const int EXTRA_SONG_TIME = 1;

    public static MusicManager Instance;

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
            StartCoroutine(MusicLoop());
        }
    }

    IEnumerator MusicLoop()
    {
        AudioSO currentMusic = GetRandomSong();

        audioSource.clip = currentMusic.GetAudioClip();
        audioSource.volume = currentMusic.GetVolume();

        audioSource.Play();

        yield return new WaitForSeconds(currentMusic.GetClipLength() + EXTRA_SONG_TIME);
        audioSource.Stop();

        StartCoroutine(MusicLoop());
    }

    AudioSO GetRandomSong()
    {
        return allSongs[Random.Range(0, allSongs.Length)];
    }
}
