using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade/BallsBackPercent")]
public class BallsBackPercent : UpgradeSO
{
    [SerializeField] [Range(0f, 1f)] float ballsBackPercent = 0f;
    public override void OnEquip()
    {
        EventManager.OnMonsterDead += EventManager_OnMonsterDead; ;
    }

    public override void OnRemove()
    {
        EventManager.OnMonsterDead -= EventManager_OnMonsterDead;
    }

    private void EventManager_OnMonsterDead(Monster monster)
    {
        if (Random.value < ballsBackPercent)
        {
            StaticSpawner.PlaySFX("ballsBack");
            GameManager.player.ChangeHealth(-1);
        }
    }

    public override string ToString()
    {
        return "+" + (ballsBackPercent * 100f) + "% Balls Back";
    }
}