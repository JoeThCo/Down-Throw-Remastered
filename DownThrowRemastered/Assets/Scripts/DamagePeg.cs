using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamagePeg : Peg
{
    int damage;

    public override void Awake()
    {
        base.Awake();
        damage = Random.Range(1, 6);
    }

    public override void OnPegHit(Ball ball)
    {
        base.OnPegHit(ball);
        ball.ChangeDamage(damage);
    }

    public override void OnPegDeath()
    {
        base.OnPegDeath();
    }
}