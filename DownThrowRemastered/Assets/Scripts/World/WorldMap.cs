using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMap : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] float removalPercent;
    [Space(10)]
    [SerializeField] Vector2Int dimensions;
    [SerializeField] Vector2Int offset;
    [Space(10)]
    [SerializeField] Transform LineCanvas;
    [SerializeField] Transform NodeCanvas;
    [Space(10)]
    public WorldPlayer WorldPlayer;

    public static WorldMap Instance;
    public static WorldNode CurrentWorldNode;

    public void Init()
    {
        Instance = this;
        StaticSpawner.Load();

        ClearMap();
        Graph graph = MakeWorldGraph();
    }

    void ClearMap()
    {
        foreach (Transform t in LineCanvas)
        {
            Destroy(t.gameObject);
        }

        foreach (Transform t in NodeCanvas)
        {
            Destroy(t.gameObject);
        }
    }

    public Graph MakeWorldGraph()
    {
        Graph graph = new Graph();
        float scale = 5f;

        graph.MakeNodes(dimensions, offset, scale);
        graph.ConnectToClosestKNeighbors(4, scale * 1.5f);
        graph.RemoveEdges(removalPercent);

        graph.SpawnAllNodes(NodeCanvas);
        graph.SpawnAllEdges(LineCanvas);

        return graph;
    }

    public void SetPlayerStartPosition(WorldNode worldNode)
    {
        WorldPlayer.transform.position = worldNode.node.Position;
        CurrentWorldNode = worldNode;
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            SceneManager.LoadScene("WorldMap");
        }
    }
}