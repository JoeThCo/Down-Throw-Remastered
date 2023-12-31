using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CurrentMonsterUI : MonoBehaviour, IUIInit
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image icon;

    [SerializeField] TextMeshProUGUI healthText;

    public static CurrentMonsterUI Instance;

    public void Init()
    {
        Instance = this;

        EventManager.OnNewMonster += EventManager_OnNewMonster;
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        EventManager.OnNewMonster -= EventManager_OnNewMonster;
    }

    private void EventManager_OnNewMonster(Monster monster)
    {
        SetCurrentMonsterUI(monster);
    }

    void SetCurrentMonsterUI(Monster monster)
    {
        nameText.SetText(monster.Name);
        icon.sprite = monster.Icon;

        UpdateCurrentMonsterUI(monster);
    }

    public void UpdateCurrentMonsterUI(Monster monster)
    {
        healthText.SetText(monster.Health + "/" + monster.GetMaxHealth());
    }
}