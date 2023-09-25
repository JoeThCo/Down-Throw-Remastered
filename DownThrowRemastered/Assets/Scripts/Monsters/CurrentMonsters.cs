using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CurrentMonsters
{
    List<Monster> monsters = new List<Monster>();

    public CurrentMonsters(int count)
    {
        monsters = MakeMonsterList(count);
        PrintMonsters();
        EventManager.Invoke(CustomEvent.NewMonster, GetTopMonster());

        EventManager.OnMonsterDamage += EventManager_OnMonsterDamage;
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        EventManager.OnMonsterDamage -= EventManager_OnMonsterDamage;
    }

    List<Monster> MakeMonsterList(int count)
    {
        List<Monster> monsterList = new List<Monster>();

        for (int i = 0; i < count; i++)
        {
            monsterList.Add(new Monster(GameManager.GetRandomMonster()));
        }

        return monsterList;
    }

    private void EventManager_OnMonsterDamage(Ball ball)
    {
        int damage = Mathf.Min(ball.damage, GetTopMonster().GetHealth());

        if (damage == 0)
        {
            ItemSpawner.PlaySFX("noMonsterDamage");
        }

        GetTopMonster().ChangeHealth(damage);
        CurrentMonsterUI.Instance.UpdateCurrentMonsterUI(GetTopMonster());

        EventManager.Invoke(CustomEvent.ScoreChange, damage);

        if (!GetTopMonster().isDead())
        {
            ItemSpawner.PlaySFX("monsterDamage");
            return;
        }

        EventManager.Invoke(CustomEvent.ScoreChange, GetTopMonster().GetMaxHealth() * GameManager.MONSTER_DEFEAT_MULTIPLIER);

        RemoveTopMonster();

        if (isAnotherMonster())
        {
            ItemSpawner.PlaySFX("monsterDefeat");
            EventManager.Invoke(CustomEvent.NewMonster, GetTopMonster());
        }
        else
        {
            EventManager.OnMonsterDamage -= EventManager_OnMonsterDamage;
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
        return monsters.Count > 0;
    }

    public void PrintMonsters()
    {
        Debug.LogWarning("Current Monsters");
        foreach (Monster current in monsters)
        {
            Debug.Log(current.GetDebugString());
        }
    }
}