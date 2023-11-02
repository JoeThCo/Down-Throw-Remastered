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
    List<ItemSlotUI> inventoryItemSlots = new List<ItemSlotUI>();

    public static ItemSlotUI selectedSlot = null;

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

        AddItem(MAX_INVENTORY_SLOTS);
    }

    void SpawnSlots(int count, Transform parent)
    {
        for (int i = 0; i < count; i++)
        {
            ItemSlotUI slot = StaticSpawner.SpawnUI("ItemSlot", parent).GetComponent<ItemSlotUI>();
            slot.Init(slotCount);
            slotCount++;

            inventoryItemSlots.Add(slot);
        }
    }

    public static Item[] GetEquipItems()
    {
        return allItems.ToList().Take(MAX_EQUIP_SLOTS).ToArray();
    }

    public void OnSelect(ItemSlotUI itemSlot)
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

    bool CanSwap(ItemSlotUI a, ItemSlotUI b)
    {
        if (a.Item == null || b.Item == null) return true;

        bool sameItemSlot = a.Item.Slot == b?.Item.Slot || b.Item.Slot == a?.Item.Slot;
        bool crossSame = a.Item.Slot == b?.EquipSlot || b.Item.Slot == a?.EquipSlot;

        Debug.Log("SameItem: " + sameItemSlot);
        Debug.Log("CrossSame: " + crossSame);

        return sameItemSlot && crossSame;
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
            newItem = new Item((ItemSlot)firstEmpty, Rarity.Common);
        }
        else
        {
            newItem = new Item();
        }

        allItems[firstEmpty] = newItem;
        inventoryItemSlots[firstEmpty].SetItem(newItem);
    }
}