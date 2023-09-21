using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : Being
{
    const float BALLS_BACK_RATE = .33f;

    public Player(int health) : base(health)
    {
        EventManager.OnPlayerShoot += EventManager_OnPlayerShoot;
        EventManager.OnMonsterDead += EventManager_OnMonsterDead;
    }

    private void EventManager_OnMonsterDead(Monster monster)
    {
        ChangeHealth((int)((float)monster.GetHealth() * BALLS_BACK_RATE));
        AimerUI.Instance.SetBallsLeftText(this);
    }

    private void EventManager_OnPlayerShoot()
    {
        ChangeHealth(1);
        AimerUI.Instance.SetBallsLeftText(this);
    }

    public override void OnDeath()
    {
        base.OnDeath();
        EventManager.Invoke(CustomEvent.GameOver);
    }
}