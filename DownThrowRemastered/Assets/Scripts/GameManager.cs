using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] PegBoard pegSpawner;

    private void Start()
    {
        ItemSpawner.Load();
        pegSpawner.SpawnBoard();
    }
}