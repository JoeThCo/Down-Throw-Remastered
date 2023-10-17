using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PrimeTween;

public class WorldNode : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nodeText;

    private Node node;

    public void Init(Node node, int Start, int End)
    {
        this.node = node;

        MakeMonsters(node, Start, End);

        SetText(node, Start, End);
        SetColor(node, Start, End);

        if (node.Id == Start)
        {
            WorldManager.Instance.SetPlayerStartPosition(node);
        }
    }

    void MakeMonsters(Node node, int Start, int End)
    {
        if (node.Id != Start || node.Id != End)
        {
            node.MakeMonsters();
        }
    }

    void SetText(Node node, int Start, int End)
    {
        if (node.Id == Start)
        {
            nodeText.SetText("Start");
        }
        else if (node.Id == End)
        {
            nodeText.SetText("End");
        }
        else
        {
            /*
            if (node.MonstersToSpawn != 0)
            {
                nodeText.SetText(node.MonstersToSpawn.ToString());
            }
            else
            {
                nodeText.gameObject.SetActive(false);
            }
            */
            nodeText.SetText(node.Id.ToString());
        }
    }

    void SetColor(Node node, int Start, int End)
    {
        if (node.Id == Start)
        {
            image.color = Color.white;
        }
        else if (node.Id == End)
        {
            image.color = Color.green;
        }
        else
        {
            if (node.MonstersToSpawn != 0)
            {
                image.color = Color.red;
            }
            else
            {
                image.color = Color.gray;
            }
        }
    }

    public void MovePlayer()
    {
        WorldManager.Instance.WorldPlayer.MovePlayer(node);
    }
}