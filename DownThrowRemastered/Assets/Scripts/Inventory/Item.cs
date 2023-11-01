using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public WhatItemSlot Slot { get; private set; }
    public ItemRarity Rarity { get; private set; }
    public List<Upgrade> Upgrades { get; private set; }

    public Color color = Color.white;

    public Item(WhatItemSlot slot, ItemRarity rarity)
    {
        Slot = slot;
        Rarity = rarity;
        Upgrades = MakeUpgrades();
        color = Random.ColorHSV();
    }

    List<Upgrade> MakeUpgrades()
    {
        return new List<Upgrade>();
    }
}