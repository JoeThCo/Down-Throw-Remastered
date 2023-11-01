using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private int maxSlots = 0;

    public List<Item> Items { get; private set; }

    public Inventory(int maxSlots)
    {
        Items = new List<Item>();
        this.maxSlots = maxSlots;
    }

    public bool AddItem(Item item)
    {
        if (Items.Count < maxSlots)
        {
            Items.Add(item);
            return true; // Item was successfully added
        }
        else
        {
            // Inventory is full, item cannot be added
            return false;
        }
    }

    public bool RemoveItem(Item item)
    {
        return Items.Remove(item);
    }

    public bool IsFull()
    {
        return Items.Count >= maxSlots;
    }

    public int Count() { return Items.Count; }
}