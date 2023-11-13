using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WorldEdge : MonoBehaviour
{
    [SerializeField] LineRenderer lineRenderer;

    public void ConnectNodes(Edge edge)
    {
        lineRenderer.positionCount = 2;

        lineRenderer.SetPosition(0, new Vector3(edge.A.Position.x, edge.A.Position.y));
        lineRenderer.SetPosition(1, new Vector3(edge.B.Position.x, edge.B.Position.y));
    }
}