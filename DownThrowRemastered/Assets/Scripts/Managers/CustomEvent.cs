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

    AreaClear,

    ScoreChange,
    HighScoreChange,

    GameOver,
    YouWin
}