using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [Space(10)]
    [SerializeField] Transform equipSlotsParent;
    [SerializeField] Transform inventorySlotsParent;

    static Item[] allItems = new Item[MAX_EQUIP_SLOTS + MAX_INVENTORY_SLOTS];
    List<ItemSlot> inventoryItemSlots = new List<ItemSlot>();

    public static ItemSlot selectedSlot = null;

    public const int MAX_EQUIP_SLOTS = 4;
    public const int MAX_INVENTORY_SLOTS = 6;

    public static InventoryManager Instance;
    int slotCount = 0;

    private void Awake()
    {
        StaticSpawner.Load();
        Instance = this;

        SpawnSlots(MAX_EQUIP_SLOTS, equipSlotsParent);
        SpawnSlots(MAX_INVENTORY_SLOTS, inventorySlotsParent);

        AddItem(MAX_EQUIP_SLOTS);
    }

    void SpawnSlots(int count, Transform parent)
    {
        for (int i = 0; i < count; i++)
        {
            ItemSlot slot = StaticSpawner.SpawnUI("ItemSlot", parent).GetComponent<ItemSlot>();
            slot.Init(slotCount);
            slotCount++;

            inventoryItemSlots.Add(slot);
        }
    }

    public static Item[] GetEquipItems()
    {
        return allItems.ToList().Take(MAX_EQUIP_SLOTS).ToArray();
    }

    public void OnSelect(ItemSlot itemSlot)
    {
        if (selectedSlot == null)
        {
            selectedSlot = itemSlot;
        }
        else if (selectedSlot == itemSlot)
        {
            selectedSlot = null;
        }
        else
        {
            if (!CanSwap(selectedSlot, itemSlot))
            {
                selectedSlot = null;
                return;
            }

            Item tempItem = itemSlot.Item;
            itemSlot.SetItem(selectedSlot.Item);
            selectedSlot.SetItem(tempItem);

            allItems[itemSlot.Index] = selectedSlot.Item;
            allItems[selectedSlot.Index] = tempItem;

            selectedSlot = null;
        }
    }

    bool CanSwap(ItemSlot a, ItemSlot b)
    {
        if (a.Item == null || b.Item == null) return true;

        return a.Item.Slot == b?.Item.Slot || b.Item.Slot == a?.Item.Slot &&
            a.EquipSlot == b?.EquipSlot || b.EquipSlot == a?.EquipSlot;
    }

    int GetFirstEmpty()
    {
        for (int i = 0; i < allItems.Length; i++)
        {
            if (allItems[i] == null)
            {
                return i;
            }
        }

        return -1;
    }

    public void AddItem(int count)
    {
        for (int i = 0; i < Mathf.Min(MAX_EQUIP_SLOTS + MAX_INVENTORY_SLOTS, count); i++)
        {
            AddItem();
        }
    }

    public void AddItem()
    {
        int firstEmpty = GetFirstEmpty();
        if (firstEmpty == -1) return;

        Item newItem;

        if (firstEmpty < MAX_EQUIP_SLOTS)
        {
            newItem = new Item((WhatItemSlot)firstEmpty, ItemRarity.Common);
        }
        else
        {
            newItem = new Item();
        }

        allItems[firstEmpty] = newItem;
        inventoryItemSlots[firstEmpty].SetItem(newItem);
    }
}