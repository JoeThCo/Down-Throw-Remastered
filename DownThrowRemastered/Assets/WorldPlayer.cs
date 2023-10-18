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
        StartCoroutine(MovePlayerI(node));
    }

    IEnumerator MovePlayerI(Node node)
    {
        if (!currentNode.IsConnectedTo(node)) yield break;

        float distance = Vector2.Distance(transform.position, node.Position);
        float duration = distance / Speed;

        Tween.Position(WorldMap.Instance.WorldPlayer.transform, endValue: node.Position, duration: duration, ease: Ease.Linear);

        yield return new WaitForSeconds(duration);

        SetCurrentNode(node);

        if (node.MonstersToSpawn > 0)
        {
            GameManager.Instance.LoadArea(node.MonstersToSpawn);
        }
    }
}