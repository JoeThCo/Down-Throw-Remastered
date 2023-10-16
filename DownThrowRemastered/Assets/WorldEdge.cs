using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEdge : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;

    public void ConnectNodes(Node a, Node b)
    {
        lineRenderer.positionCount = 2;

        lineRenderer.SetPosition(0, new Vector3(a.Position.x, a.Position.y));
        lineRenderer.SetPosition(1, new Vector3(b.Position.x, b.Position.y));
    }
}