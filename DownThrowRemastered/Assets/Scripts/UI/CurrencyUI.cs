using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyUI : MonoBehaviour, IUIInit
{
    [SerializeField] TextMeshProUGUI goldText;

    public static CurrencyUI Instance;

    public void Init()
    {
        Instance = this;

        SetGoldText(PlayFabPlayerInfo.playerAccountInfo.gold);
    }

    public void SetGoldText(int gold)
    {
        goldText.SetText(gold.ToString() + "G");
    }
}