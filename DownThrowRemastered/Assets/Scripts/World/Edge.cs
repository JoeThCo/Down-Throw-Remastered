using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    public Node a { get; }
    public Node b { get; }
    public float Weight { get; }

    public Edge(Node a, Node b)
    {
        this.a = a;
        this.b = b;
        this.Weight = Random.value;
    }
}
