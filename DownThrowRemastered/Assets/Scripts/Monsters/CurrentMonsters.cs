using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class CurrentMonsters
{
    List<Monster> monsters = new List<Monster>();

    public int Count { get; private set; }

    public CurrentMonsters(int count)
    {
        monsters = MakeMonsterList(count);
        NextMonsterUI.Instance.UpdateNextMonsters(this);

        EventManager.Invoke(CustomEvent.NewMonster, GetTopMonster());

        EventManager.OnMonsterDamage += EventManager_OnMonsterDamage;
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        EventManager.OnMonsterDamage -= EventManager_OnMonsterDamage;
    }

    public List<Monster> GetNextMonsters()
    {
        List<Monster> nextMonsters = new List<Monster>(monsters);
        nextMonsters.RemoveAt(0);

        return nextMonsters;
    }

    List<Monster> MakeMonsterList(int count)
    {
        List<Monster> monsterList = new List<Monster>();

        for (int i = 0; i < count; i++)
        {
            monsterList.Add(new Monster(StaticSpawner.GetMonsterSO()));
        }

        Count = count;
        return monsterList;
    }

    private void EventManager_OnMonsterDamage(Ball ball)
    {
        int damage = Mathf.Min(ball.damage, GetTopMonster().Health);
        if (damage == 0)
        {
            StaticSpawner.PlaySFX("noMonsterDamage");
        }

        GetTopMonster().ChangeHealth(damage);
        CurrentMonsterUI.Instance.UpdateCurrentMonsterUI(GetTopMonster());

        EventManager.Invoke(CustomEvent.ScoreChange, damage);

        if (!GetTopMonster().isDead())
        {
            StaticSpawner.PlaySFX("monsterDamage");
            return;
        }

        EventManager.Invoke(CustomEvent.ScoreChange, GetTopMonster().GetMaxHealth() * GameManager.MONSTER_DEFEAT_MULTIPLIER);
        RemoveTopMonster();

        if (isAnotherMonster())
        {
            StaticSpawner.PlaySFX("monsterDefeat");
            EventManager.Invoke(CustomEvent.NewMonster, GetTopMonster());
            NextMonsterUI.Instance.UpdateNextMonsters(this);
        }
        else
        {
            EventManager.OnMonsterDamage -= EventManager_OnMonsterDamage;
            EventManager.Invoke(CustomEvent.NodeClear);
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