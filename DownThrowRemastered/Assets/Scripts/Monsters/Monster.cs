using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monster : Being
{
    protected int maxHealth;

    public Monster(MonsterSO monsterSO)
    {
        this.health = Mathf.FloorToInt(monsterSO.monster.health * GameManager.CurrentDifficulty);
        this.beingName = monsterSO.name;
        this.icon = monsterSO.monster.icon;

        maxHealth = health;
    }

    public int GetMaxHealth() { return maxHealth; }

    public string GetDebugString()
    {
        return beingName + " H: " + health;
    }

    public override void OnDeath()
    {
        base.OnDeath();
        EventManager.Invoke(CustomEvent.MonsterDead, this);
    }
}