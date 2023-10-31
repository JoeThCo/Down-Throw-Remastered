using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RarityProperties
{
    public static int GetUpgradePoints(ItemRarity rarity)
    {
        switch (rarity)
        {
            case ItemRarity.Common: return 3;
            case ItemRarity.Uncommon: return 6;
            case ItemRarity.Rare: return 9;
            case ItemRarity.Epic: return 12;
            case ItemRarity.Legendary: return 15;
            default: return 0;
        }
    }

    public static int GetBuffMax(ItemRarity rarity)
    {
        switch (rarity)
        {
            case ItemRarity.Common:
            case ItemRarity.Uncommon: return 1;
            case ItemRarity.Rare:
            case ItemRarity.Epic: return 2;
            case ItemRarity.Legendary: return 3;
            default: return 0;
        }
    }

    public static Color GetColor(ItemRarity rarity)
    {
        switch (rarity)
        {
            case ItemRarity.Common: return Color.gray;
            case ItemRarity.Uncommon: return Color.green;
            case ItemRarity.Rare: return Color.blue;
            case ItemRarity.Epic: return Color.magenta;
            case ItemRarity.Legendary: return Color.yellow;
            default: return Color.white;
        }
    }
}