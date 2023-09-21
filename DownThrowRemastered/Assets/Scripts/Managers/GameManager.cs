using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ballsLeft;
    [Space(10)]
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
        EventManager.OnPlayerShoot += EventManager_OnPlayerShoot;
    }

    private void EventManager_OnPlayerShoot()
    {
        ballsLeft.SetText(player.GetHealth().ToString());
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
        ballsLeft.SetText(player.GetHealth().ToString());
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