using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticSpawner
{
    static GameObject[] allGameObjects;
    static GameObject[] allUI;

    static MonsterSO[] allMonstersSOs;
    static AudioSO[] allSFXAudioSO;
    static BackgroundSO[] allBackgroundSO;

    public static void Load()
    {
        allGameObjects = Resources.LoadAll<GameObject>("GameObjects");
        allUI = Resources.LoadAll<GameObject>("UI");

        allMonstersSOs = Resources.LoadAll<MonsterSO>("Monsters");
        allSFXAudioSO = Resources.LoadAll<AudioSO>("SFX");
        allBackgroundSO = Resources.LoadAll<BackgroundSO>("Backgrounds");
    }

    static ScriptableObject GetScriptableObject(ScriptableObject[] input, string name)
    {
        foreach (ScriptableObject current in input)
        {
            if (current.name.Equals(name))
            {
                return current;
            }
        }

        throw new System.Exception(name + " is not found!");
    }

    static GameObject GetPrefab(GameObject[] input, string name)
    {
        foreach (GameObject current in input)
        {
            if (current.name.Equals(name))
            {
                return current;
            }
        }

        throw new System.Exception(name + " is not found!");
    }

    static GameObject GetGameObjectPrefab(string name)
    {
        return GetPrefab(allGameObjects, name);
    }

    static GameObject GetUIPrefab(string name)
    {
        return GetPrefab(allUI, name);
    }

    #region Game OBJ
    public static GameObject SpawnGame(string name, Vector3 position)
    {
        GameObject go = null;
        GameObject game = GetGameObjectPrefab(name);
        if (game)
        {
            go = Object.Instantiate(game, position, Quaternion.identity);
        }

        return go;
    }

    public static GameObject SpawnGame(string name, Vector3 position, Transform parent)
    {
        GameObject go = null;
        GameObject game = GetGameObjectPrefab(name);
        if (game)
        {
            go = Object.Instantiate(game, position, Quaternion.identity, parent);
        }

        return go;
    }

    #endregion

    public static BackgroundSO GetBackgroundSO(string name)
    {
        return GetScriptableObject(allBackgroundSO, name) as BackgroundSO;
    }

    public static BackgroundSO GetBackgroundSO()
    {
        return allBackgroundSO[Random.Range(0, allBackgroundSO.Length)];
    }

    public static MonsterSO GetMonsterSO()
    {
        return allMonstersSOs[Random.Range(0, allMonstersSOs.Length)];
    }

    public static void PlaySFX(string name)
    {
        SoundEffect sfx = Object.Instantiate(GetGameObjectPrefab("SFX"), Vector3.zero, Quaternion.identity).GetComponent<SoundEffect>();
        sfx.Init(GetScriptableObject(allSFXAudioSO, name) as AudioSO);
    }

    public static GameObject SpawnText(Vector3 position, string info)
    {
        GameObject go = null;
        GameObject ui = GetUIPrefab("TextObject");
        if (ui)
        {
            go = Object.Instantiate(ui, position, Quaternion.identity);
            go.GetComponent<TextObject>().Init(info);
        }

        return go;
    }

    public static GameObject SpawnText(Vector3 position, string info, float destroyTime)
    {
        GameObject go = null;
        GameObject ui = GetUIPrefab("TextObject");
        if (ui)
        {
            go = Object.Instantiate(ui, position, Quaternion.identity);
            go.GetComponent<TextObject>().Init(info, destroyTime);
        }

        return go;
    }

    public static GameObject SpawnUI(string name, Transform parent)
    {
        GameObject go = null;
        GameObject ui = GetUIPrefab(name);
        if (ui)
        {
            go = Object.Instantiate(ui, Vector3.zero, Quaternion.identity, parent);
        }

        return go;
    }
}
