using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class SettingsUI : MonoBehaviour, IUIInit
{
    [SerializeField] SettingSliderUI music;
    [SerializeField] SettingSliderUI sfx;
    [Space(10)]
    [SerializeField] SettingSliderUI powerSensitivity;
    [SerializeField] SettingSliderUI aimSensitivtiy;
    [Space(10)]
    [SerializeField] SettingDropDownUI aimType;

    public void Init()
    {
        music.Init(SettingsManager.GetMusicVolume());
        sfx.Init(SettingsManager.GetSFXVolume());

        powerSensitivity.Init(SettingsManager.GetPowerSensitivity());
        aimSensitivtiy.Init(SettingsManager.GetAimSensitivity());

        aimType.Init(SettingsManager.GetAimType());
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