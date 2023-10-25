using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class WorldMap : MonoBehaviour
{
    [SerializeField] [Range(0f, 1f)] float scale = 5f;
    [SerializeField] [Range(0f, 5f)] float distanceCutOffMultiplier = 1.5f;
    [Space(10)]
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
    public static WorldNode CurrentWorldNode { get; private set; }

    public void Init()
    {
        Instance = this;
        StaticSpawner.Load();

        ClearMap();
        MakeWorldGraph();
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

    public void MakeWorldGraph()
    {
        ClearMap();

        Graph graph = new Graph();

        graph.MakeNodes(dimensions, offset, scale);
        graph.ConnectToClosestKNeighbors(Node.MAX_CONNECTIONS, scale * distanceCutOffMultiplier);
        graph.RemoveEdges(removalPercent);

        graph.SpawnAllNodes(NodeCanvas);
        graph.SpawnAllEdges(LineCanvas);
    }

    public void SetPlayerStartPosition(WorldNode worldNode)
    {
        WorldPlayer.transform.position = worldNode.node.Position;
        CurrentWorldNode = worldNode;
    }

    public static void SetCurrentNode(WorldNode worldNode)
    {
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