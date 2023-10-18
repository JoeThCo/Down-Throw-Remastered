using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Node
{
    public int MonstersToSpawn { get; private set; }
    public int Id { get; }
    public Vector2 Position { get; }
    public List<Edge> Edges { get; } = new List<Edge>();

    private const float MONSTER_SPAWN_CHANCE = .65f;
    private const float SCALE_MOVEMENT = .15f;

    public Node(int id, int x, int y, float scale)
    {
        Id = id;

        Position = (Vector2.right * x + Vector2.up * y) * scale + (Random.insideUnitCircle * (scale * SCALE_MOVEMENT));
    }

    public void MakeMonsters()
    {
        if (Random.value < MONSTER_SPAWN_CHANCE)
        {
            MonstersToSpawn = Random.Range(0, GameManager.MAX_MONSTERS + 1);
        }
    }

    public bool IsConnectedTo(Node targetNode)
    {
        if (targetNode == this) return false;

        foreach (var edge in Edges)
        {
            if (edge.a == targetNode || edge.b == targetNode)
            {
                return true;
            }
        }

        return false;
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

    public List<Node> GetNeighbors()
    {
        List<Node> neighbors = new List<Node>();
        foreach (var edge in Edges)
        {
            if (edge.a == this)
                neighbors.Add(edge.b);
            else if (edge.b == this)
                neighbors.Add(edge.a);
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