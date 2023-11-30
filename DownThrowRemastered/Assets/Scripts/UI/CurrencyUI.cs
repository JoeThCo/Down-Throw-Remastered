using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PrimeTween;
using UnityEngine.SceneManagement;
using System;

public class CurrencyUI : MonoBehaviour, IUIInit
{
    [SerializeField] TextMeshProUGUI goldText;

    [SerializeField] float scoreShake = 1.5f;
    [SerializeField] float scoreTime = .35f;

    public static CurrencyUI Instance;

    public void Init()
    {
        Instance = this;
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
        EventManager.OnGoldChange += EventManager_OnGoldChange;

        SetGoldText();
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        SceneManager.sceneUnloaded -= SceneManager_sceneUnloaded;
        EventManager.OnGoldChange -= EventManager_OnGoldChange;
    }

    private void EventManager_OnGoldChange(int change)
    {
        SetGoldText();
        goldJuice();
    }

    void SetGoldText()
    {
        goldText.SetText(GameManager.player.gold.ToString() + "G");
    }

    void goldJuice()
    {
        Tween.ShakeScale(transform, Vector3.one * scoreShake, scoreTime);
    }
}