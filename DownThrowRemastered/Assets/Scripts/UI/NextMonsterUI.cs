using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NextMonsterUI : MonoBehaviour
{
    public static NextMonsterUI Instance;

    public void Init()
    {
        Instance = this;
    }

    public void UpdateNextMonsters(CurrentMonsters currentMonsters)
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }

        foreach (Monster monster in currentMonsters.GetNextMonsters())
        {
            NextMonster nextMonster = ItemSpawner.SpawnUI("NextMonsterObject", transform).GetComponent<NextMonster>();
            nextMonster.Init(monster);
        }
    }
}