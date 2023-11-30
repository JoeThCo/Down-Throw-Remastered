using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using PrimeTween;

public class TextObject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;
    [SerializeField] float scaleJuice = 1.05f;
    [SerializeField] float positionJuice = 1.25f;


    private const float DEFAULT_DESTROY_TIME = .5f;

    public void Init(string info, float destoryTime = DEFAULT_DESTROY_TIME)
    {
        text.SetText(info);
        textJuice(destoryTime);
        Destroy(gameObject, destoryTime);
    }

    public void Init(string info, Color color, float destoryTime = DEFAULT_DESTROY_TIME)
    {
        text.SetText(info);
        textJuice(destoryTime);
        SetColor(color);
        Destroy(gameObject, destoryTime);
    }

    void SetColor(Color color)
    {
        text.color = color;
    }

    void textJuice(float time)
    {
        Sequence.Create()
            .Group(Tween.ShakeLocalPosition(transform, Vector3.one * positionJuice, time))
            .Group(Tween.Scale(transform, Vector3.one * scaleJuice, time));
    }
}