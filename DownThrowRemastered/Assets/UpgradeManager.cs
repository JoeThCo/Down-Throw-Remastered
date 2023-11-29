using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class UpgradeManager
{
    public const float NEW_ITEM_CHANCE = .25f;

    static Dictionary<UpgradeSO, int> upgradesDict;
    static List<UpgradeSO> upgradesList;


    public static void Init()
    {
        upgradesDict = new Dictionary<UpgradeSO, int>();
        upgradesList = new List<UpgradeSO>();

        EventManager.OnNodeClear += EventManager_OnNodeClear;
    }

    private static void EventManager_OnNodeClear()
    {
        if (Random.value < NEW_ITEM_CHANCE) return;
        AddUpgrade(StaticSpawner.GetUpgradeSO(Helpers.BiasEnumValue<Rarity>()));
    }

    public static void AddUpgrade(UpgradeSO toAdd)
    {
        ListAdd(toAdd);
        DictAdd(toAdd);
    }

    static void ListAdd(UpgradeSO toAdd)
    {
        upgradesList.Add(toAdd);
        toAdd.OnEquip();
    }

    static void DictAdd(UpgradeSO toAdd)
    {
        if (upgradesDict.ContainsKey(toAdd))
        {
            upgradesDict[toAdd]++;
            Debug.Log(toAdd.name + ": " + upgradesDict[toAdd]);
        }
        else
        {
            upgradesDict[toAdd] = 1;
            Debug.Log("New item: " + toAdd.name);
        }
    }
}