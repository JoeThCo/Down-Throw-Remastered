using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/New AudioSO")]
public class AudioSO : ScriptableObject
{
    [SerializeField] AudioClip audioClip;
    [SerializeField] [Range(0f, 1f)] float volume;

    public AudioClip GetAudioClip()
    {
        return audioClip;
    }

    public float GetVolume()
    {
        return volume;
    }

    public float GetClipLength()
    {
        return audioClip.length;
    }
}