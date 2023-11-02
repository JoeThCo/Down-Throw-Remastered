using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlotUI : MonoBehaviour
{
    [SerializeField] Button button;
    [Space(10)]
    [SerializeField] Image buttonBackground;
    [SerializeField] Image itemImage;
    public Item Item { get; set; }
    public int Index { get; private set; }
    public ItemSlot EquipSlot { get; private set; }

    public void OnButtonPress()
    {
        InventoryManager.Instance.OnSelect(this);
    }

    public void Init(int index, bool isInteractable = true)
    {
        this.Index = index;
        button.interactable = isInteractable;

        SetEquipItemSlot();
    }

    void SetEquipItemSlot()
    {
        if (IsEquipSlot())
        {
            EquipSlot = (ItemSlot)Index;
        }
        else
        {
            EquipSlot = ItemSlot.None;
        }
    }

    public void SetItem(Item newItem)
    {
        Item = newItem;

        if (Item == null)
        {
            buttonBackground.color = Color.white;
            itemImage.sprite = null;
        }
        else
        {
            Item.OnDeEquip();

            buttonBackground.color = RarityProperties.GetColor(newItem.Rarity);
            itemImage.sprite = StaticSpawner.GetSprite(newItem.Slot.ToString());

            if (!IsEquipSlot()) return;
            Item.OnEquip();
        }
    }

    public bool IsEquipSlot()
    {
        return Index < Enum.GetNames(typeof(ItemSlot)).Length - 1;
    }

    public void OnHoverEnter()
    {
        if (Item == null) return;

        SideBarUI.Instance.UpdateHoverItemText(Item);
    }

    public void OnHoverExit()
    {
        SideBarUI.Instance.UpdateHoverItemText();
    }
}