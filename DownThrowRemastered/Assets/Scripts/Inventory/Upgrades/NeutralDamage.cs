using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade/NeutralDamage")]
public class NeutralDamage : PercentUpgradeSO
{
    public override void OnEquip()
    {
        EventManager.OnPegHit += EventManager_OnPegHit;
    }

    public override void OnRemove()
    {
        EventManager.OnPegHit -= EventManager_OnPegHit;
    }

    private void EventManager_OnPegHit(Peg peg)
    {
        if (!hasUpgradeEffectHit()) return;

        EventManager.Invoke(CustomEvent.MonsterDamage, 1);
    }

    public override string ToString()
    {
        return "+" + PercentToString() + " Neutral Damage %";
    }
}
