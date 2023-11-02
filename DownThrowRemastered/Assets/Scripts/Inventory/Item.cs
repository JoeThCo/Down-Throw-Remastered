using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemSlot Slot { get; private set; }
    public Rarity Rarity { get; private set; }
    public List<UpgradeSO> Upgrades { get; private set; }
    public Color color { get; private set; }

    public Item()
    {
        Slot = Helpers.RandomEnumValue<ItemSlot>(1);
        Rarity = Helpers.RandomEnumValue<Rarity>();

        Upgrades = MakeUpgrades();
        color = Random.ColorHSV();
    }

    public Item(Rarity rarity)
    {
        Slot = Helpers.RandomEnumValue<ItemSlot>(1);

        Rarity = rarity;
        Upgrades = MakeUpgrades();
        color = Random.ColorHSV();
    }

    public Item(ItemSlot slot, Rarity rarity)
    {
        Slot = slot;

        Rarity = rarity;
        Upgrades = MakeUpgrades();
        color = Random.ColorHSV();
    }

    List<UpgradeSO> MakeUpgrades()
    {
        List<UpgradeSO> output = new List<UpgradeSO>();

        int totalCost = RarityProperties.GetUpgradePoints(Rarity);

        int iterations = 0;
        while (totalCost > 0)
        {
            UpgradeSO newUpgrade = ScriptableObject.Instantiate(StaticSpawner.GetUpgradeSO(RarityProperties.GetRarity()));
            totalCost -= RarityProperties.GetBuffCost(newUpgrade.ItemRarity);
            output.Add(newUpgrade);

            iterations++;
            if (iterations > 25)
            {
                Debug.LogError("Iteration max!");
                return output;
            }
        }

        return output;
    }
}