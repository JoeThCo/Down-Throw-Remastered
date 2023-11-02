using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Being
{
    public Sprite Icon { get; protected set; }
    public string Name { get; protected set; }
    public int Health { get; protected set; }
    
    public Being()
    {
        Health = 1;
    }

    public Being(int health)
    {
        this.Health = health;
    }

    public bool isDead()
    {
        return Health <= 0;
    }

    public void ChangeHealth(int change)
    {
        Health -= change;

        if (Health <= 0)
        {
            OnDeath();
        }
    }

    public virtual void OnDeath()
    {

    }
}
