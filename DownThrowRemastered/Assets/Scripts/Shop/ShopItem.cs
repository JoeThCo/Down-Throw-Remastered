using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Shop Item")]
public class ShopItem : ScriptableObject
{
    public Sprite icon;
    public int Cost;
}