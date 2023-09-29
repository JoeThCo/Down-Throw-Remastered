using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField] float basePower = 10f;
    [SerializeField] [Range(1f, 10f)] float playerPowerRange = 7.5f;
    [Space(10)]
    [SerializeField] Transform firePoint;

    [HideInInspector] public bool canShoot = true;

    private const float MIN_POWER_SCALED = .1f;
    private const float MAX_POWER_SCALED = 1f;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
    }

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
        ball.velocity = -firePoint.transform.up * (GetPlayerPower() * basePower);

        ItemSpawner.PlaySFX("playerShoot");
        canShoot = false;
    }
    float GetPlayerPower()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        float power = (firePoint.position.y - mousePos.y) / playerPowerRange;
        return Mathf.Clamp(power, MIN_POWER_SCALED, MAX_POWER_SCALED);
    }

    private void Update()
    {
        if (Input.GetMouseButtonUp(0) && canShoot)
        {
            EventManager.Invoke(CustomEvent.PlayerShootStart);
        }
    }
}