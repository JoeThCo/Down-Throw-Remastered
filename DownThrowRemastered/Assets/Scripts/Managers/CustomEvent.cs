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

    NodeEnter,

    NodeClear,
    WorldClear,

    ScoreChange,
    HighScoreChange,

    GameOver,
    YouWin,

    GoldChange
}