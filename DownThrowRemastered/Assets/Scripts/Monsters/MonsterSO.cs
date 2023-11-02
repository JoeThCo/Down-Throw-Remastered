using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/New Monster")]
public class MonsterSO : ScriptableObject
{
    public Monster monster;
    public Sprite GetSprite() { return monster.Icon; }
}