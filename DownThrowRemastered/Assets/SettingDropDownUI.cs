using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingDropDownUI : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropDown;

    public void Init(int value) 
    {
        SetAimType(value);
    }

    public void SetAimType()
    {
        SettingsManager.SetAimType(dropDown.value);
    }

    public void SetAimType(int value)
    {
        dropDown.value = value;
        SettingsManager.SetAimType(value);
    }
}