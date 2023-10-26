using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class PlayerShopInfo
{
    [SerializeField] public static string hat { get; set; }

    [SerializeField] public static List<string> ownedItems = new List<string>();

    public void SetItem(ShopItemSO shopItemSO)
    {
        switch (shopItemSO.ItemType)
        {
            case ItemType.Hat:
                hat = shopItemSO.name;
                break;

            default:
                Debug.LogError("Set Item Issue!");
                break;
        }
    }

    public bool HasItem(ShopItemSO shopItemSO)
    {
        return ownedItems.Contains(shopItemSO.name);
    }

    public void AddItem(ShopItemSO shopItemSO)
    {
        ownedItems.Add(shopItemSO.name);
        PlayFabPlayerInfo.ChangeGold(shopItemSO.Gold);
    }
}