using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField] float basePower = 10f;
    [Space(10)]
    [SerializeField] Transform ballParent;
    [SerializeField] Transform firePoint;

    [HideInInspector] public bool canShoot = true;
    private float playerPower = HALF_POWER;

    private const float MIN_POWER = .1f;
    private const float HALF_POWER = .5f;
    private const float MAX_POWER = 1f;

    private void Start()
    {
        AimUI.Instance.SetBarPower(playerPower);
    }

    private void OnEnable()
    {
        EventManager.OnPlayerShootStart += EventManager_onPlayerShoot;
        EventManager.OnBallBottoms += EventManager_OnBallBottoms;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerShootStart -= EventManager_onPlayerShoot;
        EventManager.OnBallBottoms -= EventManager_OnBallBottoms;
    }

    private void EventManager_OnBallBottoms(Ball ball)
    {
        canShoot = true;
    }

    private void EventManager_onPlayerShoot()
    {
        Rigidbody2D ball = StaticSpawner.SpawnGame("Ball", firePoint.transform.position, ballParent).GetComponent<Rigidbody2D>();
        ball.velocity = -firePoint.transform.up * (playerPower * basePower);

        StaticSpawner.PlaySFX("playerShoot");
        canShoot = false;
    }

    void PlayerPower()
    {
        float vert = Input.GetAxisRaw("Vertical");

        if (vert == 0) return;
        if (vert > 0)
        {
            playerPower += Time.deltaTime;
        }
        else
        {
            playerPower -= Time.deltaTime;
        }

        playerPower = Mathf.Clamp(playerPower, MIN_POWER, MAX_POWER);
        AimUI.Instance.SetBarPower(playerPower);
    }

    bool isCorrectPlayerControlsToShoot()
    {
        return Input.GetMouseButtonDown(0);
    }

    private void FixedUpdate()
    {
        if (!canShoot) return;

        PlayerPower();
    }

    private void Update()
    {
        if (!canShoot) return;

        if (isCorrectPlayerControlsToShoot())
        {
            EventManager.InvokePlayerShootStart();
        }
    }
}