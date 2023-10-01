using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class NextMonster : MonoBehaviour
{
    [SerializeField] Image icon;

    public void Init(Monster monster)
    {
        SetIcon(monster.GetIcon());
    }

    void SetIcon(Sprite sprite)
    {
        icon.sprite = sprite;
    }
}