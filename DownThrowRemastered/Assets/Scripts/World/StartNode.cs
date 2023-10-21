using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartNode : WorldNode
{
    public override void Init(Node node)
    {
        base.Init(node);

        WorldMap.Instance.SetPlayerStartPosition(this);
    }

    public override void OnEnterNode()
    {
        base.OnEnterNode();
    }
}