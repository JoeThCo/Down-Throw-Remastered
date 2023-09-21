using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PegBoard pegSpawner;

    static Player player;
    static CurrentMonsters currentMonsters;

    static MonsterSO[] allMonsters;

    private void Start()
    {
        Load();

        PlayerInit();
        CurrentMonsterInit();

        pegSpawner.SpawnBoard();

        EventManager.OnAreaClear += EventManager_OnAreaClear;
    }

    private void EventManager_OnAreaClear()
    {
        Debug.Log("Area clear!");
    }

    void Load()
    {
        ItemSpawner.Load();
        LoadMonsters();
    }

    void LoadMonsters()
    {
        allMonsters = Resources.LoadAll<MonsterSO>("Monsters");
    }

    void PlayerInit()
    {
        player = new Player(15);
    }

    void CurrentMonsterInit()
    {
        currentMonsters = new CurrentMonsters(1);
    }

    public static MonsterSO GetRandomMonster()
    {
        return allMonsters[Random.Range(0, allMonsters.Length)];
    }
}