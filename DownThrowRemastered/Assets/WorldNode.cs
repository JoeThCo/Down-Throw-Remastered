using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class WorldNode : MonoBehaviour
{
    [SerializeField] Image image;
    [SerializeField] TextMeshProUGUI nodeText;

    public void Init(Node node, int Start, int End)
    {
        MakeMonsters(node, Start, End);

        SetText(node, Start, End);
        SetColor(node, Start, End);
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
            if (node.MonstersToSpawn != 0)
            {
                nodeText.SetText(node.MonstersToSpawn.ToString());
            }
            else
            {
                nodeText.gameObject.SetActive(false);
            }
        }
    }

    void SetColor(Node node, int Start, int End)
    {
        if (node.Id == Start)
        {
            image.color = Color.blue;
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
}