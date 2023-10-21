using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterNode : WorldNode
{
    [SerializeField] Image onCompleteCheckMark;
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

        if (MonstersToSpawn <= 0) return;
        
        GameManager.Instance.LoadNode(MonstersToSpawn);
        MenuManager.Instance.DisplayMenus("Game");
    }

    public void MakeMonsters()
    {
        MonstersToSpawn = Random.Range(1, GameManager.MAX_MONSTERS + 1);
    }

    public void OnNodeClear()
    {
        nodeText.gameObject.SetActive(false);
        onCompleteCheckMark.gameObject.SetActive(true);

        MonstersToSpawn = 0;
    }
}