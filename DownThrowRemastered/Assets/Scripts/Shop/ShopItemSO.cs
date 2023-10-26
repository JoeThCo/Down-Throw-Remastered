using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Shop Item")]
public class ShopItemSO : ScriptableObject
{
    public ItemType ItemType;
    public Sprite Icon;
    public int Gold;
}

public enum ItemType
{
    Hat
}