using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundEffect : MonoBehaviour
{
    [SerializeField] AudioSource audioSource;

    public void Init(AudioSO audioSO)
    {
        audioSource.clip = audioSO.audioClip;
        audioSource.volume = audioSO.GetVolume();

        audioSource.Play();

        Destroy(gameObject, audioSO.GetClipLength() * 2);
    }
}