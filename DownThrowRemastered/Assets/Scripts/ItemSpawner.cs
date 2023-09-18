using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ItemSpawner
{
    static GameObject[] allGameObjects;

    public static void Load()
    {
        allGameObjects = Resources.LoadAll<GameObject>("GameObjects");
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

    public static GameObject SpawnGame(string name, Vector3 position)
    {
        GameObject go = null;
        GameObject prefab = GetGameObjectPrefab(name);
        if (prefab)
        {
            go = Object.Instantiate(prefab, position, Quaternion.identity);
        }

        return go;
    }

    public static GameObject SpawnGame(string name, Vector3 position, Transform parent)
    {
        GameObject go = null;
        GameObject prefab = GetGameObjectPrefab(name);
        if (prefab)
        {
            go = Object.Instantiate(prefab, position, Quaternion.identity, parent);
        }

        return go;
    }
}
