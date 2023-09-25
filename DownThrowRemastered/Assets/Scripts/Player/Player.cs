using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class Player : Being
{
    const float BALLS_BACK_RATE = .5f;
    const int MINIMUM_BALLS_BACK = 1;

    public Player(int health) : base(health)
    {
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
    }

    private void EventManager_OnPlayerShootEnd()
    {
        if (!isDead()) return;
        EventManager.Invoke(CustomEvent.GameOver);
    }

    private void EventManager_OnMonsterDead(Monster monster)
    {
        int ballsBack = Mathf.FloorToInt((float)monster.GetHealth() * BALLS_BACK_RATE) + MINIMUM_BALLS_BACK;
        ChangeHealth(-ballsBack);
        AimerUI.Instance.SetBallsLeftText(this);
        Debug.Log("+" + ballsBack + " balls back for " + monster.GetName());
    }

    private void EventManager_OnPlayerShootStart()
    {
        ChangeHealth(1);
        AimerUI.Instance.SetBallsLeftText(this);
    }
}