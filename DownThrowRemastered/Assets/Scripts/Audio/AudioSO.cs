using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/New AudioSO")]
public class AudioSO : ScriptableObject
{
    public AudioClip audioClip;
    [SerializeField] [Range(0f, 1f)] float volume;

    public float GetVolume()
    {
        return volume;
    }

    public float GetClipLength()
    {
        return audioClip.length;
    }
}