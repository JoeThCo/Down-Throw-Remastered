using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using PrimeTween;

public class CameraManager : MonoBehaviour
{
    [SerializeField] Camera cam;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneUnloaded += SceneManager_sceneUnloaded;

        EventManager.OnPlayerShootStart += EventManager_OnPlayerShootStart;
        EventManager.OnMonsterDamage += EventManager_OnMonsterDamage;
    }

    private void SceneManager_sceneUnloaded(Scene arg0)
    {
        EventManager.OnPlayerShootStart -= EventManager_OnPlayerShootStart;
        EventManager.OnMonsterDamage -= EventManager_OnMonsterDamage;
    }

    private void EventManager_OnPlayerShootStart()
    {
        Tween.ShakeCamera(cam, 2f);
    }

    private void EventManager_OnMonsterDamage(int damage)
    {
        if (damage <= 0) return;
        Tween.ShakeCamera(cam, .5f);
    }
}
