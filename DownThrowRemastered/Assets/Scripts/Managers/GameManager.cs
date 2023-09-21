using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] PegBoard pegSpawner;
    [SerializeField] AimerUI aimerUI;

    static Player player;
    static CurrentMonsters currentMonsters;

    static MonsterSO[] allMonsters;

    private void Start()
    {
        Load();
        Init();

        pegSpawner.SpawnBoard();

        EventManager.OnAreaClear += EventManager_OnAreaClear;
        EventManager.OnYouWin += EventManager_OnYouWin;
        EventManager.OnGameOver += EventManager_OnGameOver;
    }

    private void EventManager_OnYouWin()
    {
        MenuManager.Instance.DisplayMenus("YouWin");
    }

    private void EventManager_OnGameOver()
    {
        MenuManager.Instance.DisplayMenus("GameOver");
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

    void Init()
    {
        aimerUI.Init();
        PlayerInit();
        CurrentMonsterInit();
    }

    void LoadMonsters()
    {
        allMonsters = Resources.LoadAll<MonsterSO>("Monsters");
    }

    void PlayerInit()
    {
        player = new Player(10);
        AimerUI.Instance.SetBallsLeftText(player);
    }

    void CurrentMonsterInit()
    {
        currentMonsters = new CurrentMonsters(3);
    }

    public static MonsterSO GetRandomMonster()
    {
        return allMonsters[Random.Range(0, allMonsters.Length)];
    }
}