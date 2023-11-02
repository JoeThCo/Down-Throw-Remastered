using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RarityProperties
{
    public static int GetUpgradePoints(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common: return 1;
            case Rarity.Uncommon: return 3;
            case Rarity.Rare: return 5;
            case Rarity.Epic: return 7;
            case Rarity.Legendary: return 9;
            default: return 0;
        }
    }

    public static int GetBuffCost(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common: return 1;
            case Rarity.Uncommon: return 2;
            case Rarity.Rare: return 3;
            case Rarity.Epic: return 4;
            case Rarity.Legendary: return 5;
            default: return 0;
        }
    }

    public static Color GetColor(Rarity rarity)
    {
        switch (rarity)
        {
            case Rarity.Common: return Color.gray;
            case Rarity.Uncommon: return Color.green;
            case Rarity.Rare: return Color.blue;
            case Rarity.Epic: return Color.magenta;
            case Rarity.Legendary: return Color.yellow;
            default: return Color.white;
        }
    }

    public static Rarity GetRarity()
    {
        float randValue = UnityEngine.Random.value; // Returns a random number between 0.0 [inclusive] and 1.0 [exclusive]

        if (randValue < 0.50f) // 50% chance
            return Rarity.Common;
        else if (randValue < 0.75f) // 25% chance
            return Rarity.Uncommon;
        else if (randValue < 0.90f) // 15% chance
            return Rarity.Rare;
        else if (randValue < 0.97f) // 7% chance
            return Rarity.Epic;
        else // 3% chance
            return Rarity.Legendary;
    }
}