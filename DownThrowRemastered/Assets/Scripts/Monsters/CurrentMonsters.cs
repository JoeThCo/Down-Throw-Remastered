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
        EventManager.Invoke(CustomEvent.NewMonster, GetTopMonster());
        PrintMonsters();

        EventManager.OnMonsterDamage += EventManager_OnMonsterDamage;
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        EventManager.OnMonsterDamage -= EventManager_OnMonsterDamage;
    }

    List<Monster> MakeMonsterList(int count)
    {
        List<Monster> temp = new List<Monster>();

        for (int i = 0; i < count; i++)
        {
            temp.Add(new Monster(GameManager.GetRandomMonster()));
        }

        return temp;
    }

    public void DebugMonsterSize()
    {
        Debug.Log("Monsters left: " + monsters.Count);
    }

    private void EventManager_OnMonsterDamage(Ball ball)
    {
        int damage = Mathf.Min(ball.damage, GetTopMonster().GetHealth());
        GetTopMonster().ChangeHealth(damage);
        CurrentMonsterUI.Instance.UpdateCurrentMonsterUI(GetTopMonster());

        EventManager.Invoke(CustomEvent.ScoreChange, damage);

        if (!GetTopMonster().isDead()) return;
        EventManager.Invoke(CustomEvent.ScoreChange, GetTopMonster().GetMaxHealth() * GameManager.MONSTER_DEFEAT_MULTIPLIER);

        Debug.Log("Top Removed");
        RemoveTopMonster();
        DebugMonsterSize();

        if (isAnotherMonster())
        {
            EventManager.Invoke(CustomEvent.NewMonster, GetTopMonster());
        }
        else
        {
            EventManager.Invoke(CustomEvent.AreaClear);
            EventManager.Invoke(CustomEvent.YouWin);
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
        foreach (Monster current in monsters)
        {
            Debug.Log(current.GetDebugString());
        }
    }
}