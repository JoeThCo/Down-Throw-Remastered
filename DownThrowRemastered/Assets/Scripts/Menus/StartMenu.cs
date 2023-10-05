using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartMenu : MonoBehaviour
{
    [SerializeField] BackgroundManager backgroundManager;
    [Space(10)]
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI versionText;
    [SerializeField] TextMeshProUGUI companyText;
    [Space(10)]
    [SerializeField] TextMeshProUGUI usernameText;


    private void Awake()
    {
        PlayFabInfo.OfflinePlay();
        ItemSpawner.Load();

        titleText.SetText(Application.productName);
        versionText.SetText(Application.version);
        companyText.SetText(Application.companyName);

        usernameText.SetText(PlayFabInfo.GetName());

        backgroundManager.Init();
    }
}