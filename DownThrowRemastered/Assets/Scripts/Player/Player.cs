using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : Being
{
    const float BALLS_BACK_RATE = .33f;

    public Player(int health) : base(health)
    {
        EventManager.OnPlayerShoot += EventManager_OnPlayerShootStart;
        EventManager.OnPlayerShootEnd += EventManager_OnPlayerShootEnd;
        EventManager.OnMonsterDead += EventManager_OnMonsterDead;
    }

    ~Player()
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
        ChangeHealth((int)((float)monster.GetHealth() * BALLS_BACK_RATE));
        AimerUI.Instance.SetBallsLeftText(this);
    }

    private void EventManager_OnPlayerShootStart()
    {
        ChangeHealth(1);
        AimerUI.Instance.SetBallsLeftText(this);
    }
}