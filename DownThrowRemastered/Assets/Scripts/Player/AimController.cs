using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField] float basePower = 10f;
    [Space(10)]
    [SerializeField] Transform ballParent;
    [SerializeField] Transform firePoint;
    [Space(10)]
    [SerializeField] AimUI aimUI;

    [HideInInspector] public bool canShoot = true;
    private float playerPower = HALF_POWER;

    private const float MIN_POWER = .1f;
    private const float HALF_POWER = .5f;
    private const float MAX_POWER = 1f;

    private void Start()
    {
        aimUI.SetBarPower(playerPower);
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
            playerPower += Time.deltaTime * SettingsManager.GetPowerSensitivity();
        }
        else
        {
            playerPower -= Time.deltaTime * SettingsManager.GetPowerSensitivity();
        }

        playerPower = Mathf.Clamp(playerPower, MIN_POWER, MAX_POWER);
        aimUI.SetBarPower(playerPower);
    }

    bool isCorrectPlayerControlsToShoot()
    {
        int aimType = SettingsManager.GetAimType();

        return aimType == (int)AimType.Keys && Input.GetKeyDown(KeyCode.Space) || aimType == (int)AimType.Mouse && Input.GetMouseButtonDown(0) && !MouseOverUI.isOverUI;
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
            EventManager.Invoke(CustomEvent.PlayerShootStart);
        }
    }
}