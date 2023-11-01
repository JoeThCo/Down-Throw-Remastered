using System.Collections;
using System.Collections.Generic;
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

        AddItem();
        AddItem();

        Debug.Log(slotCount);
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

    public void OnSelect(ItemSlot itemSlot)
    {
        if (selectedSlot == null)
        {
            selectedSlot = itemSlot;
        }
        else
        {
            Item tempItem = itemSlot.GetItem();
            itemSlot.SetItem(selectedSlot.GetItem());
            selectedSlot.SetItem(tempItem);

            allItems[itemSlot.GetIndex()] = selectedSlot.GetItem();
            allItems[selectedSlot.GetIndex()] = tempItem;

            selectedSlot = null;
        }
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

    public void AddItem()
    {
        int firstEmpty = GetFirstEmpty();
        if (firstEmpty == -1) return;

        Item item = new Item(WhatItemSlot.Hat, ItemRarity.Legendary);
        allItems[firstEmpty] = item;

        inventoryItemSlots[firstEmpty].SetItem(item);
    }
}