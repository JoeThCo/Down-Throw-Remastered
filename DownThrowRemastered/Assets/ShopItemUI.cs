using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ShopItemUI : MonoBehaviour
{
    [SerializeField] Image itemImage;
    [SerializeField] TextMeshProUGUI itemText;

    ShopItemSO shopItem;

    public void Init(ShopItemSO shopItemSO)
    {
        this.shopItem = shopItemSO;

        itemImage.sprite = shopItemSO.Icon;
        itemText.SetText(shopItemSO.name);
    }

    bool CanPurchaseItem()
    {
        return PlayFabPlayerInfo.playerAccountInfo.gold >= shopItem.Gold;
    }

    void PurchaseItem(ShopItemSO shopItemSO)
    {
        PlayFabPlayerInfo.playerShopInfo.AddItem(shopItemSO);
    }

    void EquipItem()
    {
        PlayFabPlayerInfo.playerShopInfo.SetItem(shopItem);
    }

    void OnEquip()
    {
        EquipItem();
    }

    void OnPurchase()
    {
        PurchaseItem(shopItem);
        PlayFabPlayerInfo.playerShopInfo.AddItem(shopItem);
    }

    public void OnButtonPress()
    {
        if (PlayFabPlayerInfo.playerShopInfo.HasItem(shopItem))
        {
            OnEquip();
        }
        else
        {
            if (!CanPurchaseItem()) return;
            OnPurchase();
        }
    }
}