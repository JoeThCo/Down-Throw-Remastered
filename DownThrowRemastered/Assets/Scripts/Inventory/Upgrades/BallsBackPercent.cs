using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade/BallsBackPercent")]
public class BallsBackPercent : PercentUpgradeSO
{
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
        if (!hasUpgradeEffectHit()) return;

        Debug.Log("Ball Back!");
        StaticSpawner.PlaySFX("ballsBack");
        GameManager.player.ChangeHealth(-1);
    }

    public override string ToString()
    {
        return "+" + PercentToString() + " Balls Back";
    }
}