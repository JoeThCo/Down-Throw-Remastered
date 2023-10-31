using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [Space(10)]
    [SerializeField] Transform equipSlots;
    [SerializeField] Transform inventorySlots;

    public static Inventory playerInventory { get; private set; }

    List<ItemSlot> playerInventorySlots;

    public const int EQUIP_SLOTS = 4;
    public const int INVENTORY_SLOTS = 6;

    private void Awake()
    {
        StaticSpawner.Load();

        playerInventorySlots = new List<ItemSlot>();
        playerInventory = new Inventory();

        SpawnSlots(EQUIP_SLOTS, equipSlots);
        SpawnSlots(INVENTORY_SLOTS, inventorySlots, true);

        AddItem(new Item(WhatItemSlot.Hat, ItemRarity.Legendary));
    }

    void SpawnSlots(int count, Transform parent, bool saveSlots = false)
    {
        for (int i = 0; i < count; i++)
        {
            ItemSlot slot = StaticSpawner.SpawnUI("Slot", parent).GetComponent<ItemSlot>();

            if (saveSlots)
            {
                playerInventorySlots.Add(slot);
            }
        }
    }

    public void AddItem(Item item)
    {
        if (!playerInventory.AddItem(item)) return;

        ItemSlot slot = playerInventorySlots[playerInventory.Count() - 1];
        DraggableItem dragItem = StaticSpawner.SpawnUI("DraggableItem").GetComponent<DraggableItem>();

        dragItem.Init(canvas);
        slot.SetItem(dragItem);
    }
}