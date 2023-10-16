using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAccountInfo
{
    public string name;
    public int highScore;

    public int gold;

    public PlayerSettings playerSettings;

    public PlayerAccountInfo()
    {
        name = "Offline";

        highScore = 0;
        gold = 0;

        playerSettings = new PlayerSettings();
        EventManager.OnCashChange += EventManager_OnCashChange;
    }

    private void EventManager_OnCashChange(int change)
    {
        gold += change;
        CurrencyUI.Instance.SetGoldText(gold);
    }

    public string ToJson()
    {
        return JsonUtility.ToJson(this);
    }
}