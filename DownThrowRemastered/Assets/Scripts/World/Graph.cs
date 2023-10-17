using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph
{
    public List<Node> Nodes { get; } = new List<Node>();
    public List<Edge> Edges { get; } = new List<Edge>();

    public int Start;
    public int End;

    void PickStartAndEnd(Vector2Int dimensions)
    {
        Start = Random.Range(0, dimensions.y);
        End = Random.Range(Nodes.Count - dimensions.y, Nodes.Count);
    }

    public void MakeNodes(Vector2Int dimensions, float scale)
    {
        int i = 0;

        for (int x = 0; x < dimensions.x; x++)
        {
            for (int y = 0; y < dimensions.y; y++)
            {
                Nodes.Add(new Node(i, x, y, scale));
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
        Debug.Log($"Edges to Remove: {percentToRemove}");

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

    void SpawnNode(Node node, Transform parent)
    {
        WorldNode worldNode = StaticSpawner.SpawnGame("WorldNode", new Vector3(node.Position.x, node.Position.y), parent).GetComponent<WorldNode>();
        worldNode.Init(node, Start, End);
    }

    void SpawnEdge(Edge edge, Transform parent)
    {
        WorldEdge worldEdge = StaticSpawner.SpawnGame("Line", Vector3.zero, parent).GetComponent<WorldEdge>();
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
}