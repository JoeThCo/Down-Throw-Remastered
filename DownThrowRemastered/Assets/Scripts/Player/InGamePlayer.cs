using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class InGamePlayer : Being
{
    public int gold { get; private set; }

    const float BALLS_BACK_RATE = .33f;
    const int MINIMUM_BALLS_BACK = 1;

    public InGamePlayer(int health) : base(health)
    {
        EventManager.OnGoldChange += EventManager_OnGoldChange;

        EventManager.OnWorldClear += EventManager_OnWorldClear;
        EventManager.OnPlayerShoot += EventManager_OnPlayerShootStart;
        EventManager.OnPlayerShootEnd += EventManager_OnPlayerShootEnd;

        EventManager.OnMonsterDead += EventManager_OnMonsterDead;

        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        EventManager.OnPlayerShoot -= EventManager_OnPlayerShootStart;
        EventManager.OnPlayerShootEnd -= EventManager_OnPlayerShootEnd;
        EventManager.OnMonsterDead -= EventManager_OnMonsterDead;

        SceneManager.sceneUnloaded -= SceneManager_sceneUnloaded;
    }

    private void EventManager_OnGoldChange(int change)
    {
        gold += change;

        CurrencyUI.Instance.SetGoldText();
    }

    private void EventManager_OnWorldClear()
    {
        ChangeHealth(-GameManager.WorldsCleared);
    }

    private void EventManager_OnPlayerShootEnd()
    {
        if (!isDead()) return;
        EventManager.Invoke(CustomEvent.GameOver);
    }

    private void EventManager_OnMonsterDead(Monster monster)
    {
        float monsterHealthFraction = (float)monster.GetMaxHealth() * BALLS_BACK_RATE;
        int ballsBack = Mathf.FloorToInt(monsterHealthFraction) + MINIMUM_BALLS_BACK;

        ChangeHealth(-ballsBack);
        AimUI.Instance.SetBallsLeftText(this);
    }

    private void EventManager_OnPlayerShootStart()
    {
        ChangeHealth(1);
        AimUI.Instance.SetBallsLeftText(this);
    }
}