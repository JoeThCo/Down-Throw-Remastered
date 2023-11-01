using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemSlot : MonoBehaviour
{
    [SerializeField] Image itemImage;
    private Item item = null;
    private int index = -1;

    public void OnButtonPress()
    {
        InventoryManager.Instance.OnSelect(this);
    }

    public void Init(int index)
    {
        this.index = index;
    }

    public void SetItem(Item newItem)
    {
        if (newItem == null)
        {
            this.item = null;
            itemImage.color = Color.white;
        }
        else
        {
            this.item = newItem;
            itemImage.color = newItem.color;
        }
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