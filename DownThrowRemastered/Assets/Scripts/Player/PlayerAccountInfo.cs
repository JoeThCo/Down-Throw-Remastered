using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerAccountInfo
{
    public string name { get; set; }
    public int highScore { get; set; }
    public int gold { get; set; }
    public PlayerSettings playerSettings { get; set; }

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