using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PercentUpgradeSO : UpgradeSO
{
    [SerializeField] [Range(0f, 1f)] protected float percent = .5f;

    public override void OnEquip()
    {
        base.OnEquip();
    }

    public override void OnRemove()
    {
        base.OnRemove();
    }

    protected bool hasUpgradeEffectHit()
    {
        return Random.value < percent;
    }

    protected string PercentToString()
    {
        return (percent * 100f).ToString("F0") + "%";
    }
}
