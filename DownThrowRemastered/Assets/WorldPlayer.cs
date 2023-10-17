using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class WorldPlayer : MonoBehaviour
{
    [SerializeField] float Speed = 5;
    public Node currentNode;

    public void SetCurrentNode(Node node)
    {
        currentNode = node;
    }

    public void MovePlayer(Node node)
    {
        if (!currentNode.IsConnectedTo(node)) return;

        float distance = Vector2.Distance(transform.position, node.Position);
        Tween.Position(WorldMap.Instance.WorldPlayer.transform, endValue: node.Position, duration: distance / Speed, ease: Ease.Linear);

        SetCurrentNode(node);
        node.PrintNeighbors();
    }
}