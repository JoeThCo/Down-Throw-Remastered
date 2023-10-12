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

    public static float GetAimSensitivity()
    {
        return playerSettings.aimSensitivity;
    }

    public static float GetPowerSensitivity()
    {
        return playerSettings.powerSensitivity;
    }

    public static void SetPowerSensitvity(float value)
    {
        playerSettings.powerSensitivity = value;
    }

    public static void SetAimSensitvity(float value)
    {
        playerSettings.aimSensitivity = value;
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

    public float powerSensitivity;
    public float aimSensitivity;

    public PlayerSettings()
    {
        musicVolume = .25f;
        sfxVolume = .75f;

        powerSensitivity = 1f;
        aimSensitivity = 1f;
    }
}