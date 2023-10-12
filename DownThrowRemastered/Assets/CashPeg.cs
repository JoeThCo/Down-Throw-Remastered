using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CashPeg : Peg
{
    int pegWorth = 1;

    public override void OnPegHit()
    {
        base.OnPegHit();
    }

    public override void OnPegDeath(Ball ball)
    {
        base.OnPegDeath(ball);

        EventManager.Invoke(CustomEvent.CashChange, pegWorth);
    }
}