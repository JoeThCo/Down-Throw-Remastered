using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CurrentMonsterUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] Image icon;

    [SerializeField] TextMeshProUGUI healthText;

    private void OnEnable()
    {
        EventManager.OnNewMonster += EventManager_OnNewMonster;

        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        EventManager.OnNewMonster -= EventManager_OnNewMonster;
    }

    private void EventManager_OnNewMonster()
    {
        Debug.Log("Update ui!");
        SetCurrentMonsterUI(GameManager.currentMonsters.GetTopMonster());
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
}