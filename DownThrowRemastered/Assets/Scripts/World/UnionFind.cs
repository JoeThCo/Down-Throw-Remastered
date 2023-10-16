using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnionFind
{
    private int[] parent;
    private int[] rank;

    public UnionFind(int size)
    {
        parent = new int[size];
        rank = new int[size];

        for (int i = 0; i < size; i++)
        {
            parent[i] = i;
            rank[i] = 0;
        }
    }

    public int Find(int node)
    {
        if (parent[node] != node)
        {
            parent[node] = Find(parent[node]);
        }

        return parent[node];
    }

    public bool Union(int node1, int node2)
    {
        int root1 = Find(node1);
        int root2 = Find(node2);

        if (root1 == root2)
            return false;  // nodes are already in the same set

        if (rank[root1] < rank[root2])
        {
            parent[root1] = root2;
        }
        else if (rank[root1] > rank[root2])
        {
            parent[root2] = root1;
        }
        else
        {
            parent[root2] = root1;
            rank[root1]++;
        }

        return true;
    }
}
