using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePeg : Peg
{
    [SerializeField] TextMeshProUGUI damageText;
    int damage;

    private void Awake()
    {
        SetDamage(Random.Range(1, 6));
    }

    void SetDamage(int damage)
    {
        this.damage = damage;
        damageText.SetText(damage.ToString());
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