using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : Being
{
    public Monster()
    {

    }

    public Monster(int health) : base(health)
    {

    }

    public override void OnDeath()
    {
        base.OnDeath();
        EventManager.Invoke(CustomEvent.MonsterDeath);
    }
}
