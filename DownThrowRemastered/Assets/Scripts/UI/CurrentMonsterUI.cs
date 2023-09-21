using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CurrentMonsterUI : MonoBehaviour
{
    [SerializeField] CanvasGroup canvasGroup;
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image icon;

    [SerializeField] TextMeshProUGUI healthText;

    public static CurrentMonsterUI Instance;

    private void Awake()
    {
        Instance = this;
        EventManager.OnNewMonster += EventManager_OnNewMonster;
        EventManager.OnAreaClear += EventManager_OnAreaClear;
    }

    private void EventManager_OnAreaClear()
    {
        //SetVisablity(false);
    }

    private void EventManager_OnNewMonster(Monster monster)
    {
        SetCurrentMonsterUI(monster);
    }

    void SetCurrentMonsterUI(Monster monster)
    {
        nameText.SetText(monster.GetName());
        icon.sprite = monster.GetIcon();

        UpdateCurrentMonsterUI(monster);
    }

    public void UpdateCurrentMonsterUI(Monster monster)
    {
        healthText.SetText(monster.GetHealth() + "/" + monster.GetMaxHealth());
    }

    public void SetVisablity(bool state)
    {
        canvasGroup.alpha = state ? 1 : 0;
    }
}