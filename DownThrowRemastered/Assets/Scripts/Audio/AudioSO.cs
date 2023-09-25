using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/New AudioSO")]
public class AudioSO : ScriptableObject
{
    public AudioClip audioClip;
    [SerializeField] [Range(0f, 1f)] float volume;

    /// <summary>
    /// Converts Volume enum to a 0f to 1f range
    /// </summary>
    /// <returns></returns>
    public float GetVolume()
    {
        return volume;
    }


    /// <summary>
    /// Audio clip length
    /// </summary>
    /// <returns></returns>
    public float GetClipLength()
    {
        return audioClip.length;
    }
}