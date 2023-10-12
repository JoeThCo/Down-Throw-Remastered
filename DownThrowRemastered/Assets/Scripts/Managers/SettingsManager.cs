using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class SettingsManager
{
    static PlayerSettings playerSettings = new PlayerSettings();

    public static float GetMusicVolume() { return playerSettings.musicVolume; }
    public static float GetSFXVolume() { return playerSettings.sfxVolume; }

    public static void SetMusicVolume(float volume)
    {
        playerSettings.musicVolume = volume;
    }

    public static void SetSFXVolume(float volume)
    {
        playerSettings.sfxVolume = volume;
    }
}

public class PlayerSettings
{
    public float musicVolume = 1f;
    public float sfxVolume = 1f;

    public PlayerSettings()
    {
        musicVolume = .25f;
        sfxVolume = .75f;
    }
}