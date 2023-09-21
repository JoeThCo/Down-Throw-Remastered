using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Being
{
    [SerializeField] protected Sprite icon;
    protected string beingName;
    [SerializeField] protected int health;

    public int GetHealth() { return health; }
    public Sprite GetIcon() { return icon; }
    public string GetName() { return beingName; }

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

    public void ChangeHealth(int change)
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
