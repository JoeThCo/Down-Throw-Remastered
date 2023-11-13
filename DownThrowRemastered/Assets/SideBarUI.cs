using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class SideBarUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI itemStatsText;

    public static SideBarUI Instance;

    public void Init()
    {
        Instance = this;
        UpdateHoverItemText();
    }

    public void UpdateHoverItemText(Item item)
    {
        string updateText = "";

        updateText += item.ToString() + "\n";

        foreach (UpgradeSO upgrade in item.Upgrades)
        {
            updateText += upgrade.ToString() + "\n";
        }

        itemStatsText.SetText(updateText);
    }

    public void UpdateHoverItemText()
    {
        itemStatsText.SetText("");
    }
}