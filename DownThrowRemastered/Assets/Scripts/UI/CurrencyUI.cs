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
        SetGoldText();
    }

    public void SetGoldText()
    {
        goldText.SetText(GameManager.player.gold.ToString() + "G");
    }
}