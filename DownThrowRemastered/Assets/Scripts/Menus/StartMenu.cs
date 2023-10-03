using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class StartMenu : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI titleText;
    [SerializeField] TextMeshProUGUI versionText;
    [SerializeField] TextMeshProUGUI companyText;

    static bool isSaveLoaded = false;

    private void Awake()
    {
        titleText.SetText(Application.productName);
        versionText.SetText(Application.version);
        companyText.SetText(Application.companyName);

        LoadSaveOnce();
    }

    void LoadSaveOnce()
    {
        if (isSaveLoaded) return;

        SaveManager.LoadSave();
        isSaveLoaded = true;
    }
}