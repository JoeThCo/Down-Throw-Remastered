using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph
{
    public List<Node> Nodes { get; } = new List<Node>();
    public List<Edge> Edges { get; } = new List<Edge>();
    public int Start { get; private set; }
    public int End { get; private set; }
    private const float MONSTER_SPAWN_CHANCE = .5f;

    void PickStartAndEnd(Vector2Int dimensions)
    {
        Start = Random.Range(0, dimensions.y);
        End = Random.Range(Nodes.Count - dimensions.y, Nodes.Count);
    }

    public void MakeNodes(Vector2Int dimensions, Vector2 offset, float scale)
    {
        int i = 0;

        for (int x = 0; x < dimensions.x; x++)
        {
            for (int y = 0; y < dimensions.y; y++)
            {
                Nodes.Add(new Node(i, x + offset.x, y + offset.y, scale));
                i++;
            }
        }

        PickStartAndEnd(dimensions);
    }

    public void ConnectToClosestKNeighbors(int k, float scale)
    {
        foreach (var node in Nodes)
        {
            var neighbors = Nodes
                .Where(n => n != node && node.DistanceTo(n) <= scale) // Additional check for distance
                .OrderBy(n => node.DistanceTo(n))
                .Take(k)
                .ToList();

            foreach (var neighbor in neighbors)
            {
                node.ConnectTo(neighbor);
                if (!Edges.Any(e => e.a == node && e.b == neighbor || e.a == neighbor && e.b == node))
                {
                    Edges.Add(node.Edges.Last());
                }
            }
        }
    }


    public void RemoveEdges(float percent)
    {
        HashSet<Edge> mstEdges = GenerateMST();
        int totalEdgesToRemove = Edges.Count - mstEdges.Count;

        int percentToRemove = (int)((float)totalEdgesToRemove * percent);

        for (int i = 0; i < percentToRemove; i++)
        {
            RandomlyRemoveEdgeNotInMST(mstEdges);
        }
    }

    void RandomlyRemoveEdgeNotInMST(HashSet<Edge> mstEdges)
    {
        // Get edges not in the MST
        var nonMstEdges = Edges.Except(mstEdges).ToList();

        if (nonMstEdges.Count == 0)
        {
            return;
        }

        // Randomly select an edge from the nonMST edges
        Edge edgeToRemove = nonMstEdges[Random.Range(0, nonMstEdges.Count)];

        // Remove this edge
        edgeToRemove.a.DisconnectFrom(edgeToRemove);
        edgeToRemove.b.DisconnectFrom(edgeToRemove);
        Edges.Remove(edgeToRemove);
    }


    public HashSet<Edge> GenerateMST()
    {
        var result = new HashSet<Edge>();
        UnionFind uf = new UnionFind(Nodes.Count);

        // Sort edges by weight. Assuming Edge class has a Weight property.
        var sortedEdges = Edges.OrderBy(e => e.Weight).ToList();

        foreach (var edge in sortedEdges)
        {
            int nodeIndexA = Nodes.IndexOf(edge.a);
            int nodeIndexB = Nodes.IndexOf(edge.b);

            if (uf.Union(nodeIndexA, nodeIndexB))
            {
                result.Add(edge);
                if (result.Count == Nodes.Count - 1) break; // we have V-1 edges, so MST is complete
            }
        }

        return result;
    }

    private void DFS(Node node, HashSet<Node> visited)
    {
        if (visited.Contains(node)) return;
        visited.Add(node);
        foreach (var edge in node.Edges)
        {
            DFS(edge.a == node ? edge.b : edge.a, visited);
        }
    }

    public void RemoveNodesWithNoEdges()
    {
        Nodes.RemoveAll(node => !node.Edges.Any());
    }

    #region Spawning

    void SpawnNode(Node node, Transform parent)
    {
        if (node.Id == Start)
        {
            WorldNode emptyNode = StaticSpawner.SpawnGame("StartNode", new Vector3(node.Position.x, node.Position.y), parent).GetComponent<WorldNode>();
            emptyNode.Init(node);
        }
        else if (node.Id == End)
        {
            EndNode endNode = StaticSpawner.SpawnGame("EndNode", new Vector3(node.Position.x, node.Position.y), parent).GetComponent<EndNode>();
            endNode.Init(node);
        }
        else if (Random.value < MONSTER_SPAWN_CHANCE)
        {
            MonsterNode monsterNode = StaticSpawner.SpawnGame("MonsterNode", new Vector3(node.Position.x, node.Position.y), parent).GetComponent<MonsterNode>();
            monsterNode.Init(node);
        }
        else
        {
            WorldNode emptyNode = StaticSpawner.SpawnGame("EmptyNode", new Vector3(node.Position.x, node.Position.y), parent).GetComponent<WorldNode>();
            emptyNode.Init(node);
        }
    }

    void SpawnEdge(Edge edge, Transform parent)
    {
        WorldEdge worldEdge = StaticSpawner.SpawnGame("WorldEdge", Vector3.zero, parent).GetComponent<WorldEdge>();
        worldEdge.ConnectNodes(edge);
    }

    public void SpawnAllEdges(Transform parent)
    {
        foreach (Edge edge in Edges)
        {
            SpawnEdge(edge, parent);
        }
    }

    public void SpawnAllNodes(Transform parent)
    {
        foreach (Node node in Nodes)
        {
            SpawnNode(node, parent);
        }
    }

    #endregion
}