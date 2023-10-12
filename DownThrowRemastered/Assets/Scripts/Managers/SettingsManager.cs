using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SettingsManager
{
    static PlayerSettings playerSettings;

    public static void SetPlayerSettings(PlayerSettings input)
    {
        if (input == null)
        {
            playerSettings = new PlayerSettings();
        }
        else
        {
            playerSettings = input;
        }
    }

    public static float GetMusicVolume()
    {
        return playerSettings.musicVolume;
    }
    public static float GetSFXVolume()
    {
        return playerSettings.sfxVolume;
    }

    public static void SetMusicVolume(float volume)
    {
        playerSettings.musicVolume = volume;
    }

    public static void SetSFXVolume(float volume)
    {
        playerSettings.sfxVolume = volume;
    }
}

[System.Serializable]
public class PlayerSettings
{
    public float musicVolume = .5f;
    public float sfxVolume = .5f;

    public PlayerSettings()
    {
        musicVolume = .25f;
        sfxVolume = .75f;
    }
}