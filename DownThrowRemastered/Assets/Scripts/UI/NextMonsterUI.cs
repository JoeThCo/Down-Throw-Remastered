using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMonsterUI : MonoBehaviour, IUIInit
{
    public static NextMonsterUI Instance;

    public void Init()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        EventManager.OnNewMonster += EventManager_OnNewMonster;
    }

    private void OnDisable()
    {
        EventManager.OnNewMonster -= EventManager_OnNewMonster;
    }

    private void EventManager_OnNewMonster()
    {
        UpdateNextMonsters();
    }

    void UpdateNextMonsters()
    {
        Debug.Log("Update Next");
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        foreach (Monster monster in GameManager.currentMonsters.GetNextMonsters())
        {
            NextMonster nextMonster = StaticSpawner.SpawnUI("NextMonsterObject", transform).GetComponent<NextMonster>();
            nextMonster.Init(monster);
        }
    }
}