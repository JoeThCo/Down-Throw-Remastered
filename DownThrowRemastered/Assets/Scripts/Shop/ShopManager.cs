using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ShopManager
{
    static ShopItem[] allShopItems;

    public static void Load()
    {
        allShopItems = Resources.LoadAll<ShopItem>("Shop");
    }
}