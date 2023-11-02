using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EquippedItemsUI : MonoBehaviour
{
    public static EquippedItemsUI Instance;

    ItemSlotUI[] itemSlotUIs;

    public void Init()
    {
        itemSlotUIs = new ItemSlotUI[InventoryManager.MAX_EQUIP_SLOTS];
        Instance = this;
    }

    public void SpawnSlots()
    {
        for (int i = 0; i < InventoryManager.MAX_EQUIP_SLOTS; i++)
        {
            ItemSlotUI itemSlot = StaticSpawner.SpawnUI("ItemSlot", transform).GetComponent<ItemSlotUI>();
            itemSlot.Init(i, false);

            itemSlotUIs[i] = itemSlot;
        }
    }

    public void SetItemSlots()
    {
        Item[] equippedItems = InventoryManager.GetEquipItems();

        for (int i = 0; i < InventoryManager.MAX_EQUIP_SLOTS; i++)
        {
            Item current = equippedItems[i];

            if (current != null)
            {
                itemSlotUIs[i].SetItem(equippedItems[i]);
            }
        }
    }
}
