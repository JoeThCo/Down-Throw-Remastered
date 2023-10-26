using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopManager : MonoBehaviour
{
    [SerializeField] Transform itemTransfrom;
    ShopItemSO[] allShopItems;

    private void Awake()
    {
        StaticSpawner.Load();

        Load();
        SpawnShop();
    }

    public void Load()
    {
        allShopItems = Resources.LoadAll<ShopItemSO>("Shop");
    }

    void SpawnShop()
    {
        foreach (Transform t in itemTransfrom)
        {
            Destroy(t.gameObject);
        }

        foreach (ShopItemSO item in allShopItems)
        {
            ShopItemUI shopItemUI = StaticSpawner.SpawnUI("ShopItem", itemTransfrom).GetComponent<ShopItemUI>();
            shopItemUI.Init(item);
        }
    }
}