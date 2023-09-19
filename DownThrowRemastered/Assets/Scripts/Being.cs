using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Being
{
    int health;

    public Being()
    {
        health = 1;
    }

    public Being(int health)
    {
        this.health = health;
    }

    void ChangeHeath(int change)
    {
        health -= change;

        if (health <= 0)
        {
            OnDeath();
        }
    }

    public virtual void OnDeath()
    {

    }
}
