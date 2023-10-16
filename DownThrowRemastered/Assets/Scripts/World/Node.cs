using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node
{
    public int Id { get; }
    public Vector2Int Position { get; }
    public List<Edge> Edges { get; } = new List<Edge>();

    public Node(int id, int x, int y, int scale)
    {
        Id = id;
        Position = new Vector2Int(x, y) * scale;
    }

    public void ConnectTo(Node node)
    {
        if (Edges.Any(e => (e.a == this && e.b == node) || (e.a == node && e.b == this)))
            return; // Skip if already connected

        var edge = new Edge(this, node);
        Edges.Add(edge);
        node.Edges.Add(edge);
    }

    public void DisconnectFrom(Edge edge)
    {
        Edges.Remove(edge);
    }

    public double DistanceTo(Node other)
    {
        return Mathf.Sqrt(Mathf.Pow(Position.x - other.Position.x, 2) + Mathf.Pow(Position.y - other.Position.y, 2));
    }
}