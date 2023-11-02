using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Monster : Being
{
    protected int maxHealth;

    public Monster(MonsterSO monsterSO)
    {
        this.Health = Mathf.FloorToInt(monsterSO.monster.Health * GameManager.GetMonsterDifficulty());
        this.Name = monsterSO.name;
        this.Icon = monsterSO.monster.Icon;

        maxHealth = Health;
    }

    public int GetMaxHealth() { return maxHealth; }

    public string GetDebugString()
    {
        return Name + " H: " + Health;
    }

    public override void OnDeath()
    {
        base.OnDeath();
        EventManager.Invoke(CustomEvent.MonsterDead, this);
    }
}