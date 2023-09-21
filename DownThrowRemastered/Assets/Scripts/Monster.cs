using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monster : Being
{
    public Monster(MonsterSO monsterSO)
    {
        this.health = monsterSO.monster.health;
        this.beingName = monsterSO.name;
    }

    public Monster(int health) : base(health)
    {

    }

    public string GetDebugString()
    {
        return beingName + " H: " + health;
    }

    public override void OnDeath()
    {
        base.OnDeath();
        EventManager.Invoke(CustomEvent.MonsterDead);
    }
}
