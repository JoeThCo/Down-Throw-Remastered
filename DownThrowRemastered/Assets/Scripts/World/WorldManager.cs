using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldManager : MonoBehaviour
{
    public WorldPlayer WorldPlayer;

    [SerializeField] [Range(0f, 1f)] float removalPercent;
    [SerializeField] Vector2Int dimensions;

    public static WorldManager Instance;

    private void Start()
    {
        Instance = this;
        StaticSpawner.Load();

        Graph graph = MakeWorldGraph();
    }

    public Graph MakeWorldGraph()
    {
        Graph graph = new Graph();
        float scale = 5f;

        graph.MakeNodes(dimensions, scale);
        graph.ConnectToClosestKNeighbors(4, scale * 1.5f);
        graph.RemoveEdges(removalPercent);

        graph.SpawnAllNodes(transform);
        graph.SpawnAllEdges(transform);
        
        return graph;
    }

    public void SetPlayerStartPosition(Node node)
    {
        WorldPlayer.transform.position = node.Position;
        WorldPlayer.SetCurrentNode(node);

        node.PrintNeighbors();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("WorldMap");
        }
    }
}