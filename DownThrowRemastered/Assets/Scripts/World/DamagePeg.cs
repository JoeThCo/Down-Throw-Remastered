using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePeg : Peg
{
    [SerializeField] TextMeshProUGUI damageText;
    int damage;

    const int MAX_DAMAGE = 5;
    const float DAMAGE_PEG_BIAS = .5f;

    private void Awake()
    {
        SetDamage(Mathf.CeilToInt(Helpers.GetBiasNumber(DAMAGE_PEG_BIAS) * MAX_DAMAGE));
    }

    void SetDamage(int damage)
    {
        this.damage = damage;
        damageText.SetText(damage.ToString());
    }

    public override void OnPegHit()
    {
        base.OnPegHit();
    }

    public override void OnPegDeath(Ball ball)
    {
        base.OnPegDeath(ball);
        ball.ChangeDamage(damage);
        ItemSpawner.SpawnUI("TextObject", transform.position, ball.damage.ToString());
    }
}