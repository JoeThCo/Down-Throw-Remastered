using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackgroundManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer backgroundSR;
    public static BackgroundManager Instance;

    public void Init()
    {
        Instance = this;

        SetBackGround("Field");
    }

    public void SetBackGround(string name)
    {
        BackgroundSO backgroundSO = ItemSpawner.GetBackgroundSO(name);

        backgroundSR.sprite = backgroundSO.GetSprite();
        backgroundSR.transform.localScale = (Vector3.right + Vector3.up) * backgroundSO.GetScale();
    }
}
