using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InventoryManager : MonoBehaviour
{
    public static Inventory playerInventory { get; private set; }



    private void Awake()
    {
        playerInventory = new Inventory();
    }
}