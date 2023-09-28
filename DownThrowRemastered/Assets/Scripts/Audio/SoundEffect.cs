using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    private const float EXTRA_TIME = .5f;

    public void Init(AudioSO audioSO)
    {
        audioSource.clip = audioSO.GetAudioClip();
        audioSource.volume = audioSO.GetVolume();

        audioSource.Play();

        Destroy(gameObject, audioSO.GetClipLength() + EXTRA_TIME);
    }
}