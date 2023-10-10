using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/New Background")]
public class BackgroundSO : ScriptableObject
{
    [SerializeField] Sprite sprite;
    [SerializeField] float scale;

    public Sprite GetSprite() { return sprite; }
    public float GetScale() { return scale; }
}
