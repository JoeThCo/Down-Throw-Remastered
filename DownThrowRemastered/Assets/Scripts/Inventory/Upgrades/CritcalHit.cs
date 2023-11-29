using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade/CriticalHit")]
public class CritcalHit : PercentUpgradeSO
{
    public override void OnEquip()
    {
        EventManager.OnMonsterEffectDamage += EventManager_OnMonsterEffectDamage;
    }

    public override void OnRemove()
    {
        EventManager.OnMonsterEffectDamage -= EventManager_OnMonsterEffectDamage;
    }

    private void EventManager_OnMonsterEffectDamage(int ballDamage)
    {
        if (!hasUpgradeEffectHit()) return;

        Debug.Log("Crit!");
        int crit = Mathf.RoundToInt(((float)ballDamage * .5f));
        EventManager.InvokeOnMonsterDamage(crit);
    }

    public override string ToString()
    {
        return "+" + PercentToString() + " Crit";
    }
}