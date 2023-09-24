using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamagePeg : Peg
{
    [SerializeField] TextMeshProUGUI damageText;
    int damage;

    const int MAX_DAMAGE = 5;
    const float DAMAGE_PEG_BIAS = .90f;

    private void Awake()
    {
        SetDamage(Mathf.CeilToInt(Helpers.GetBiasNumber(DAMAGE_PEG_BIAS) * MAX_DAMAGE));

        SetDamage(Random.Range(1, 6));
    }

    void SetDamage(int damage)
    {
        this.damage = damage;
        damageText.SetText(damage.ToString());
    }

    public override void OnPegHit(Ball ball, Collision2D collision)
    {
        base.OnPegHit(ball, collision);
        ball.ChangeDamage(damage);
        ItemSpawner.SpawnUI("TextObject", collision.GetContact(0).point, ball.damage.ToString());
    }

    public override void OnPegDeath()
    {
        base.OnPegDeath();
    }
}