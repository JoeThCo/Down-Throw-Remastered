using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MonsterNode : WorldNode
{
    [SerializeField] Image onCompleteCheckMark;
    public int MonstersToSpawn { get; private set; }
    private const float MONSTER_SPAWN_BIAS = .35f;

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
        MenuManager.Instance.DisplayMenu("Game");
    }

    public void MakeMonsters()
    {
        int damage = Mathf.CeilToInt(Helpers.GetBiasNumber(MONSTER_SPAWN_BIAS) * GameManager.MAX_MONSTERS);
        MonstersToSpawn = Mathf.Clamp(damage, 1, GameManager.MAX_MONSTERS);
    }

    public void OnNodeClear()
    {
        nodeText.gameObject.SetActive(false);
        onCompleteCheckMark.gameObject.SetActive(true);

        MonstersToSpawn = 0;
    }
}