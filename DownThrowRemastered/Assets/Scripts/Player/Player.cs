using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Player : Being
{
    public Player(int health) : base(health)
    {
        EventManager.OnPlayerShoot += EventManager_OnPlayerShoot;
    }

    private void EventManager_OnPlayerShoot()
    {
        ChangeHeath(1);
    }
}