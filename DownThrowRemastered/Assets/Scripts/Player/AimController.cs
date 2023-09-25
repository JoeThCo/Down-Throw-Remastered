using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField] float throwPower = 5f;
    [SerializeField] Transform firePoint;

    [HideInInspector] public bool canShoot = true;

    private void OnEnable()
    {
        EventManager.OnPlayerShoot += EventManager_onPlayerShoot;
        EventManager.OnBallBottoms += EventManager_OnBallBottoms;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerShoot -= EventManager_onPlayerShoot;
        EventManager.OnBallBottoms -= EventManager_OnBallBottoms;
    }

    private void EventManager_OnBallBottoms(Ball ball)
    {
        canShoot = true;
    }

    private void EventManager_onPlayerShoot()
    {
        Rigidbody2D ball = ItemSpawner.SpawnGame("Ball", firePoint.transform.position).GetComponent<Rigidbody2D>();
        ball.velocity = -firePoint.transform.up * throwPower;

        ItemSpawner.PlaySFX("playerShoot");
        canShoot = false;
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && canShoot)
        {
            EventManager.Invoke(CustomEvent.PlayerShootStart);
        }
    }
}