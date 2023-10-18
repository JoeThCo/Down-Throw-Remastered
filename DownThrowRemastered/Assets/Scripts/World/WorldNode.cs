using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using PrimeTween;

public class WorldNode : MonoBehaviour
{
    [SerializeField] protected Image image;
    [SerializeField] protected TextMeshProUGUI nodeText;

    public Node node { get; private set; }

    public virtual void Init(Node node)
    {

    }

    public virtual void OnEnterNode()
    {

    }

    public void MovePlayer()
    {
        WorldMap.Instance.WorldPlayer.MovePlayer(this);
    }
}