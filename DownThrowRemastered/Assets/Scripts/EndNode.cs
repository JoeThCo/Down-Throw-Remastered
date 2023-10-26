using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndNode : WorldNode
{
    public override void Init(Node node)
    {
        base.Init(node);
    }

    public override void OnEnterNode()
    {
        base.OnEnterNode();
        EventManager.Invoke(CustomEvent.WorldClear);
    }
}