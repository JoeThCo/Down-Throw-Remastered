using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BackgroundManager : MonoBehaviour
{
    [SerializeField] SpriteRenderer backgroundSR;
    public static BackgroundManager Instance;

    public void Init()
    {
        Instance = this;
    }

    private void OnEnable()
    {
        EventManager.OnWorldClear += EventManager_OnWorldClear;
    }

    private void OnDisable()
    {
        EventManager.OnWorldClear -= EventManager_OnWorldClear;
    }

    private void EventManager_OnWorldClear()
    {
        SetRandomBackground();
    }

    public void SetBackground(string name)
    {
        BackgroundSO backgroundSO = StaticSpawner.GetBackgroundSO(name);

        backgroundSR.sprite = backgroundSO.GetSprite();
        backgroundSR.transform.localScale = (Vector3.right + Vector3.up) * backgroundSO.GetScale();
    }

    public void SetRandomBackground()
    {
        BackgroundSO backgroundSO = StaticSpawner.GetBackgroundSO();

        backgroundSR.sprite = backgroundSO.GetSprite();
        backgroundSR.transform.localScale = (Vector3.right + Vector3.up) * backgroundSO.GetScale();
    }
}
