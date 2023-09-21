using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [SerializeField] Menu[] allMenus;
    public static MenuManager Instance;

    private void Start()
    {
        Instance = this;
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
        SceneManager.LoadScene(sceneName);
    }
}