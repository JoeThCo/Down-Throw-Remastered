using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CurrentMonsters
{
    List<Monster> monsters;

    public CurrentMonsters(int count)
    {
        MakeMonsterList(count);
        PrintMonsters();

        EventManager.OnMonsterDamage += EventManager_OnMonsterDamage;
    }

    public void MakeMonsterList(int count)
    {
        monsters = new List<Monster>();

        for (int i = 0; i < count; i++)
        {
            monsters.Add(new Monster(GameManager.GetRandomMonster()));
        }
    }

    private void EventManager_OnMonsterDamage(Ball ball)
    {
        GetTopMonster().ChangeHeath(ball.damage);

        if (!GetTopMonster().isDead()) return;

        Debug.Log(GetTopMonster().GetDebugString() + " is Dead!");
        EventManager.Invoke(CustomEvent.MonsterDead);
        if (isAnotherMonster())
        {
            RemoveTopMonster();
        }
        else
        {
            EventManager.Invoke(CustomEvent.AreaClear);
        }
    }

    void RemoveTopMonster()
    {
        monsters.RemoveAt(0);
    }

    public Monster GetTopMonster()
    {
        return monsters[0];
    }

    bool isAnotherMonster()
    {
        return monsters.Count > 1;
    }
    public void PrintMonsters()
    {
        foreach (Monster current in monsters)
        {
            Debug.Log(current.GetDebugString());
        }
    }
}