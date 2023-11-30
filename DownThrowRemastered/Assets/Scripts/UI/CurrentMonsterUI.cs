using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using PrimeTween;

public class CurrentMonsterUI : MonoBehaviour, IUIInit
{
    [SerializeField] Image icon;

    [SerializeField] TextMeshProUGUI nameText;
    [SerializeField] TextMeshProUGUI healthText;

    public static CurrentMonsterUI Instance;

    private Vector3 resetScale;

    public void Init()
    {
        Instance = this;
        resetScale = transform.localScale;

        EventManager.OnNewMonster += EventManager_OnNewMonster;
        EventManager.OnMonsterDamage += EventManager_OnMonsterDamage;

        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        EventManager.OnNewMonster -= EventManager_OnNewMonster;
        EventManager.OnMonsterDamage -= EventManager_OnMonsterDamage;
    }

    private void EventManager_OnNewMonster(Monster monster)
    {
        SetCurrentMonsterUI(monster);
    }

    private void EventManager_OnMonsterDamage(int damage)
    {
        if (damage <= 0) return;

        float scale = 1.25f;
        float time = .25f;

        Sequence.Create()
            .Group(Tween.ShakeLocalPosition(transform, Vector3.one * scale, time))
            .Group(Tween.ShakeScale(transform, Vector3.one * scale, time))
            .Group(Tween.ShakeLocalRotation(transform, Vector3.one * scale, time))
            .Chain(Tween.Scale(transform, resetScale, time));

        Sequence.Create()
            .Group(Tween.Custom(Color.white, Color.red, time, onValueChange: newVal => nameText.color = newVal))
            .Chain(Tween.Custom(Color.red, Color.white, time, onValueChange: newVal => nameText.color = newVal));
            
        Sequence.Create()
            .Group(Tween.Custom(Color.white, Color.red, time, onValueChange: newVal => healthText.color = newVal))
            .Chain(Tween.Custom(Color.red, Color.white, time, onValueChange: newVal => healthText.color = newVal));
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