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
    private const float SCALE_MOVEMENT = .25f;

    public Node(int id, int x, int y, int scale)
    {
        Id = id;

        Position = (Vector2.right * x + Vector2.up * y) * scale + (Random.insideUnitCircle * (scale * SCALE_MOVEMENT));
    }

    public void MakeMonsters()
    {
        if (Random.value < MONSTER_SPAWN_CHANCE)
        {
            MonstersToSpawn = Random.Range(0, 6);
        }
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