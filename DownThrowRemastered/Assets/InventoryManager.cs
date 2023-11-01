using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    [SerializeField] Canvas canvas;
    [Space(10)]
    [SerializeField] Transform equipSlotsParent;
    [SerializeField] Transform inventorySlotsParent;

    static Item[] allItems;
    List<ItemSlot> inventoryItemSlots = new List<ItemSlot>();

    public static ItemSlot selectedSlot = null;

    public const int MAX_EQUIP_SLOTS = 4;
    public const int MAX_INVENTORY_SLOTS = 6;

    public static InventoryManager Instance;

    private void Awake()
    {
        StaticSpawner.Load();

        Instance = this;

        allItems = new Item[MAX_EQUIP_SLOTS + MAX_INVENTORY_SLOTS];

        UpdateSlots();
    }

    void DeleteSlots(Transform parent)
    {
        foreach (Transform t in parent)
        {
            Destroy(t.gameObject);
        }
    }

    void SpawnSlots(int count, Transform parent, bool isInvetorySlots = false)
    {
        for (int i = 0; i < count; i++)
        {
            ItemSlot slot = StaticSpawner.SpawnUI("ItemSlot", parent).GetComponent<ItemSlot>();

            if (isInvetorySlots)
            {
                inventoryItemSlots.Add(slot);
            }
        }
    }

    void UpdateSlots()
    {
        Debug.Log("Update");

        inventoryItemSlots.Clear();

        DeleteSlots(equipSlotsParent);
        DeleteSlots(inventorySlotsParent);

        SpawnSlots(MAX_EQUIP_SLOTS, equipSlotsParent);
        SpawnSlots(MAX_INVENTORY_SLOTS, inventorySlotsParent, true);
    }

    public void OnSelect(ItemSlot itemSlot)
    {
        if (selectedSlot == null)
        {
            Debug.Log("Null");
            selectedSlot = itemSlot;
        }
        else
        {
            if (itemSlot.GetIndex() == selectedSlot.GetIndex())
            {
                selectedSlot = null;

            }
            else
            {
                Debug.Log("Swap");
                itemSlot.SetItem(allItems[selectedSlot.GetIndex()]);
                selectedSlot.SetItem(allItems[itemSlot.GetIndex()]);
                UpdateSlots();
            }
        }
    }
}