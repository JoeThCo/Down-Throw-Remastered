using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public WhatItemSlot Slot { get; private set; }
    public ItemRarity Rarity { get; private set; }
    public List<Upgrade> Upgrades { get; private set; }

    public Item(WhatItemSlot slot, ItemRarity rarity)
    {
        Slot = slot;
        Rarity = rarity;
        Upgrades = MakeUpgrades();
    }

    List<Upgrade> MakeUpgrades()
    {
        return new List<Upgrade>();
    }
}