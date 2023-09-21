using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Being
{
    protected string beingName;
    [SerializeField] protected int health;

    public Being()
    {
        health = 1;
    }

    public Being(int health)
    {
        this.health = health;
    }

    public bool isDead() 
    {
        return health <= 0;
    }

    public void ChangeHeath(int change)
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
