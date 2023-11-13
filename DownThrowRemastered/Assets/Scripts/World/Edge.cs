using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Edge
{
    public Node A { get; private set; }
    public Node B { get; private set; }
    public float Weight { get; }

    public Edge(Node A, Node B)
    {
        this.A = A;
        this.B = B;

        this.Weight = Random.value;
    }
}
