using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory
{
    private readonly int maxSlots = 6;
    public List<Item> Items { get; private set; }

    public Inventory()
    {
        Items = new List<Item>();
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
}