using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class WorldPlayer : MonoBehaviour
{
    [SerializeField] float Speed = 5;

    public void MovePlayer(WorldNode worldNode)
    {
        StartCoroutine(MovePlayerI(worldNode));
    }

    IEnumerator MovePlayerI(WorldNode worldNode)
    {
        Node node = worldNode.node;

        if (!WorldMap.CurrentWorldNode.node.IsConnectedTo(node))
        {
            Debug.Log("Not connected!");
            yield break;
        }

        float distance = Vector2.Distance(transform.position, node.Position);
        float duration = distance / Speed;

        Tween.Position(WorldMap.Instance.WorldPlayer.transform, endValue: node.Position, duration: duration, ease: Ease.Linear);

        yield return new WaitForSeconds(duration);

        worldNode.OnEnterNode();
    }
}