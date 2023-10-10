using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class MessageUI : MonoBehaviour
{
    [SerializeField] Transform messageTransform;
    [Space(10)]
    [SerializeField] Image panel;
    [SerializeField] TextMeshProUGUI messageText;

    public static MessageUI Instance;

    private void Awake()
    {
        Instance = this;
        ShowMessage(false);
    }

    void ShowMessage(bool state)
    {
        messageTransform.gameObject.SetActive(state);
    }

    void SetMessage(string message)
    {
        messageText.SetText(message);
    }

    public void OnSuccess(string message)
    {
        panel.color = Color.green;

        ShowMessage(true);
        SetMessage(message);
    }

    public void OnError(string message)
    {
        panel.color = Color.red;

        ShowMessage(true);
        SetMessage("Error!\n" + message);
    }
}