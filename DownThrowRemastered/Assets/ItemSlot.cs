using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image itemImage;
    public Item Item { get; set; }
    public int Index { get; private set; }

    public WhatItemSlot EquipSlot { get; private set; }

    public void OnButtonPress()
    {
        InventoryManager.Instance.OnSelect(this);
    }

    public void Init(int index)
    {
        this.Index = index;

        SetEquipItemSlot();
    }

    void SetEquipItemSlot()
    {
        if (IsEquipSlot())
        {
            EquipSlot = (WhatItemSlot)Index;
        }
        else
        {
            EquipSlot = WhatItemSlot.None;
        }
    }

    public void SetItem(Item newItem)
    {
        this.Item = newItem;

        if (Item == null)
        {
            itemImage.color = Color.white;
            itemImage.sprite = null;
        }
        else
        {
            itemImage.color = newItem.color;
            itemImage.sprite = StaticSpawner.GetSprite(newItem.Slot.ToString());
        }
    }

    public bool IsEquipSlot()
    {
        return Index < Enum.GetNames(typeof(WhatItemSlot)).Length - 1;
    }
}