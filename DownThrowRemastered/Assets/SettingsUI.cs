using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingsUI : MonoBehaviour, IUIInit
{
    [SerializeField] SettingSliderUI music;
    [SerializeField] SettingSliderUI sfx;

    public void Init()
    {
        music.Init(SettingsManager.GetMusicVolume());
        sfx.Init(SettingsManager.GetSFXVolume());
    }

    private void Awake()
    {
        Init();
    }

    public void SaveSettings()
    {
        PlayFabInfo.SavePlayerInfo();
    }
}