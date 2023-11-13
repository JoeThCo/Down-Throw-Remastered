using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item
{
    public ItemSlot Slot { get; private set; }
    public Rarity Rarity { get; private set; }
    public List<UpgradeSO> Upgrades { get; private set; }

    public Item()
    {
        Slot = Helpers.RandomEnumValue<ItemSlot>(1);
        Rarity = Helpers.RandomEnumValue<Rarity>();

        Upgrades = MakeUpgrades();
    }

    public Item(ItemSlot slot, Rarity rarity)
    {
        Slot = slot;

        Rarity = rarity;
        Upgrades = new List<UpgradeSO>();
    }

    List<UpgradeSO> MakeUpgrades()
    {
        List<UpgradeSO> output = new List<UpgradeSO>();
        int totalCost = RarityProperties.GetUpgradePoints(Rarity);

        int iterations = 0;

        while (totalCost > 0)
        {
            Rarity rarity = RarityProperties.GetRarity();
            if (totalCost - RarityProperties.GetBuffCost(rarity) < 0) continue;

            UpgradeSO newUpgrade = ScriptableObject.Instantiate(StaticSpawner.GetUpgradeSO(rarity));

            totalCost -= RarityProperties.GetBuffCost(rarity);
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

    public override string ToString()
    {
        return "Nothing";
    }

    public void OnEquip()
    {
        foreach (UpgradeSO upgradeSO in Upgrades)
        {
            upgradeSO.OnEquip();
        }
    }

    public void OnDeEquip()
    {
        foreach (UpgradeSO upgradeSO in Upgrades)
        {
            upgradeSO.OnRemove();
        }
    }
}