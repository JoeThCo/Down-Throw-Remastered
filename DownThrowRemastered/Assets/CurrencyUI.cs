using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencyUI : MonoBehaviour, IUIInit
{
    [SerializeField] TextMeshProUGUI cashText;

    public static CurrencyUI Instance;

    public void Init()
    {
        Instance = this;
        SetCashText(0);
    }

    public void SetCashText(int cash)
    {
        cashText.SetText("$" + cash.ToString());
    }
}