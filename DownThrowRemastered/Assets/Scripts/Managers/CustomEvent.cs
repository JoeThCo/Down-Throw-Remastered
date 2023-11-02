using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum CustomEvent
{
    PegHit,
    PegDestroy,
    BoardClear,

    PlayerShootStart,
    PlayerShootEnd,

    NewMonster,
    MonsterDamage,
    MonsterDead,

    BallBottoms,

    NodeClear,
    WorldClear,

    ScoreChange,

    GameOver,
    YouWin,

    GoldChange
}