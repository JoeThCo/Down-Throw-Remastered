using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public WhatItemSlot Slot { get; private set; }
    public ItemRarity Rarity { get; private set; }
    public List<Upgrade> Upgrades { get; private set; }
    public Color color = Color.white;

    public Item() 
    {
        Slot = Helpers.RandomEnumValue<WhatItemSlot>(1);
        Rarity = Helpers.RandomEnumValue<ItemRarity>();

        Upgrades = MakeUpgrades();
        color = Random.ColorHSV();
    }

    public Item(ItemRarity rarity)
    {
        Slot = Helpers.RandomEnumValue<WhatItemSlot>(1);

        Rarity = rarity;
        Upgrades = MakeUpgrades();
        color = Random.ColorHSV();
    }

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