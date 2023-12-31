using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] string startMenu;
    [SerializeField] Menu[] allMenus;
    public static MenuManager Instance;

    private void Start()
    {
        Instance = this;
        DisplayMenus(startMenu);
    }

    public void DisplayMenus(string searchID)
    {
        foreach (Menu menu in allMenus)
        {
            menu.ShowMenu(menu.id.Equals(searchID));
        }
    }

    public void LoadAScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public void DisplayPlayerAimType()
    {
        switch (SettingsManager.GetAimType())
        {
            case (int)AimType.Keys:
                DisplayMenus("Keys");
                break;

            case (int)AimType.Mouse:
                DisplayMenus("Mouse");
                break;

            default:
                Debug.LogError("Big issue on menus");
                break;
        }
    }
}