using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "ScriptableObjects/Upgrade/BallsBackPercent")]
public class BallsBackPercent : UpgradeSO
{
    [SerializeField] [Range(0f, 1f)] float ballsBackPercent = .5f;
    public override void BuffAdded()
    {
        EventManager.OnBallBottoms += EventManager_OnBallBottoms;
    }

    public override void BuffRemoved()
    {
        EventManager.OnBallBottoms -= EventManager_OnBallBottoms;
    }

    private void EventManager_OnBallBottoms(Ball ball)
    {
        if (Random.value < ballsBackPercent)
        {
            GameManager.player.ChangeHealth(-1);
        }
    }
}