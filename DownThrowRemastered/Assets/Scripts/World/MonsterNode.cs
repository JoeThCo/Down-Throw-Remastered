using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterNode : WorldNode
{
    public int MonstersToSpawn { get; private set; }

    public override void Init(Node node)
    {
        base.Init(node);

        MakeMonsters();

        image.color = Color.red;
        nodeText.SetText(MonstersToSpawn.ToString());
    }

    public override void OnEnterNode()
    {
        base.OnEnterNode();

        GameManager.Instance.LoadNode(MonstersToSpawn);
        MenuManager.Instance.DisplayMenus("Game");
    }

    public void MakeMonsters()
    {
        MonstersToSpawn = Random.Range(1, GameManager.MAX_MONSTERS + 1);
    }
}