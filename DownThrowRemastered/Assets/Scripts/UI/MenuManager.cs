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
        DisplayMenu(startMenu);
    }

    public void DisplayMenu(string searchID)
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
                DisplayMenu("Keys");
                break;

            case (int)AimType.Mouse:
                DisplayMenu("Mouse");
                break;

            default:
                Debug.LogError("Big issue on menus");
                break;
        }
    }
}