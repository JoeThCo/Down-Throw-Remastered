using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node
{
    public int Id { get; }
    public Vector2 Position { get; }
    public List<Edge> Edges { get; } = new List<Edge>();

    public const int MAX_CONNECTIONS = 3;

    private const float SCALE_MOVEMENT = .15f;

    public Node(int id, float x, float y, float scale)
    {
        Id = id;
        Position = (Vector2.right * x + Vector2.up * y) * scale + (Random.insideUnitCircle * (scale * SCALE_MOVEMENT));
    }

    public bool IsConnectedTo(Node targetNode)
    {
        if (targetNode == this) return false;

        foreach (var edge in Edges)
        {
            if (edge.A == targetNode || edge.B == targetNode)
            {
                return true;
            }
        }

        return false;
    }

    public void ConnectTo(Node otherNode)
    {
        if (Edges.Count > MAX_CONNECTIONS) return;
        if (Edges.Any(e => (e.A == this && e.B == otherNode) || (e.A == otherNode && e.B == this))) return;

        Edge edge = new Edge(this, otherNode);
        Edges.Add(edge);
        otherNode.Edges.Add(edge);
    }

    public void DisconnectFrom(Edge edge)
    {
        if (Edges.Contains(edge))
        {
            Edges.Remove(edge);
        }
    }

    public double DistanceTo(Node other)
    {
        return Mathf.Sqrt(Mathf.Pow(Position.x - other.Position.x, 2) + Mathf.Pow(Position.y - other.Position.y, 2));
    }

    public List<Node> GetNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        foreach (var edge in Edges)
        {
            if (edge.A == this)
            {
                neighbors.Add(edge.B);
                continue;
            }

            if (edge.B == this)
            {
                neighbors.Add(edge.A);
                continue;
            }
        }

        return neighbors;
    }

    public void PrintNeighbors()
    {
        var neighbors = GetNeighbors();
        Debug.Log($"Node {Id} has the following neighbors:");
        foreach (var neighbor in neighbors)
        {
            Debug.Log($"Neighbor Id: {neighbor.Id} at Position: {neighbor.Position}");
        }
    }
}