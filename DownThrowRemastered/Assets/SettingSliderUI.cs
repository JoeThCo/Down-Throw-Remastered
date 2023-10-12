using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class SettingSliderUI : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI text;

    public void Init(float value)
    {
        SetSlider(value);
        SetText(value);
    }

    public void SetMusic()
    {
        SettingsManager.SetMusicVolume(slider.value);
        SetText(SettingsManager.GetMusicVolume());
    }

    public void SetSFX()
    {
        SettingsManager.SetSFXVolume(slider.value);
        SetText(SettingsManager.GetSFXVolume());
    }

    public void SetPower()
    {
        SettingsManager.SetPowerSensitvity(slider.value);
        SetText(SettingsManager.GetPowerSensitivity());
    }

    public void SetAimsensitivty()
    {
        SettingsManager.SetAimSensitvity(slider.value);
        SetText(SettingsManager.GetAimSensitivity());
    }

    void SetSlider(float value)
    {
        slider.value = value;
    }

    void SetText(float value)
    {
        text.SetText(CleanSliderValue(value));
    }

    string CleanSliderValue(float value)
    {
        return (value * 100).ToString("F0");
    }
}