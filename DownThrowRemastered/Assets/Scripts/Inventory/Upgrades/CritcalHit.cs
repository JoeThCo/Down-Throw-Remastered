using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade/CriticalHit")]
public class CritcalHit : UpgradeSO
{
    [SerializeField] [Range(0f, 1f)] float criticalHitPercent = 0f;

    public override void OnEquip()
    {
        EventManager.OnMonsterDamage += EventManager_OnMonsterDamage;
    }

    public override void OnRemove()
    {
        EventManager.OnMonsterDamage -= EventManager_OnMonsterDamage;
    }

    private void EventManager_OnMonsterDamage(int ballDamage)
    {
        if (Random.value < criticalHitPercent)
        {
            int crit = Mathf.RoundToInt(((float)ballDamage * .5f));
            EventManager.Invoke(CustomEvent.MonsterDamage, crit);
        }
    }
}