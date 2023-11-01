using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    private Item item;
    private int index;

    public void OnButtonPress()
    {
        InventoryManager.Instance.OnSelect(this);
    }

    public void Init(int index)
    {
        this.index = index;
    }

    public void SetItem(Item item)
    {
        this.item = item;
    }

    public Item GetItem()
    {
        return item;
    }

    public int GetIndex()
    {
        return index;
    }
}