using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PrimeTween;

public class NodeJuice : MonoBehaviour
{
    [SerializeField] float scale = 1.25f;
    [SerializeField] float time = .5f;

    public void MouseEnter()
    {
        Tween.Scale(transform, Vector3.one * scale, time);
    }

    public void MouseExit()
    {
        Tween.Scale(transform, Vector3.one, time);
    }
}
