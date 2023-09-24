using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class TextObject : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI text;

    private const float DEFAULT_DESTROY_TIME = 1;

    public void Init(string info, float destoryTime = DEFAULT_DESTROY_TIME)
    {
        text.SetText(info);
        Destroy(gameObject, destoryTime);
    }
}