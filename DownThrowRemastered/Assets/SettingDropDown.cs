using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class SettingDropDown : MonoBehaviour
{
    [SerializeField] TMP_Dropdown dropDown;

    public void SetAimType()
    {
        SettingsManager.SetAimType(dropDown.value);
    }
}