using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Graph
{
    public List<Node> Nodes { get; } = new List<Node>();
    public List<Edge> Edges { get; } = new List<Edge>();

    public void CreateAndConnectImmediateNeighbors(Vector2Int dimensions, int scale)
    {
        int i = 0;

        for (int y = 0; y < dimensions.y; y++)
        {
            for (int x = 0; x < dimensions.x; x++)
            {
                Nodes.Add(new Node(i, x, y, scale));
                i++;
            }
        }
    }

    public void ConnectToClosestKNeighbors(int k)
    {
        foreach (var node in Nodes)
        {
            var neighbors = Nodes
                .Where(n => n != node)
                .OrderBy(n => node.DistanceTo(n))
                .Take(k)
                .ToList();

            foreach (var neighbor in neighbors)
            {
                node.ConnectTo(neighbor);
                Edges.Add(node.Edges.Last());
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

        Debug.Log($"Removed edge: {edgeToRemove}. Total Edges now: {Edges.Count}");
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
        ItemSpawner.SpawnGame("Peg", new Vector3(node.Position.x, node.Position.y), parent);
    }

    void SpawnEdge(Node a, Node b, Transform parent)
    {
        WorldEdge edge = ItemSpawner.SpawnGame("Line", Vector3.zero, parent).GetComponent<WorldEdge>();
        edge.ConnectNodes(a, b);
    }

    public void SpawnAllEdges(Transform parent)
    {
        foreach (Edge edge in Edges)
        {
            SpawnEdge(edge.a, edge.b, parent);
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