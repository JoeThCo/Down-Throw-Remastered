using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] float removalPercent;
    [SerializeField] Vector2Int dimensions;

    private void Start()
    {
        StaticSpawner.Load();

        Graph graph = MakeWorldGraph();

        graph.SpawnAllNodes(transform);
        graph.SpawnAllEdges(transform);
    }

    public Graph MakeWorldGraph()
    {
        Graph graph = new Graph();

        graph.CreateAndConnectImmediateNeighbors(dimensions, 5);
        graph.ConnectToClosestKNeighbors(5);

        graph.RemoveEdges(removalPercent);
        return graph;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("WorldMap");
        }
    }
}