using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField] float basePower = 10f;
    [SerializeField] [Range(1f, 10f)] float playerPowerRange = 7.5f;
    [Space(10)]
    [SerializeField] Transform aimText;
    [SerializeField] Transform ballParent;
    [SerializeField] Transform firePoint;

    [HideInInspector] public bool canShoot = true;

    private bool isAngleSet = false;
    private bool isPowerSet = false;
    private float playerPower = 0;

    [SerializeField] AimType aimType;

    private const float MIN_POWER_SCALED = .1f;
    private const float MAX_POWER_SCALED = 1f;

    private Camera cam;

    private void Start()
    {
        cam = Camera.main;
        canShoot = true;
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
        ResetCalculated();
    }

    private void EventManager_onPlayerShoot()
    {
        Rigidbody2D ball = ItemSpawner.SpawnGame("Ball", firePoint.transform.position, ballParent).GetComponent<Rigidbody2D>();
        ball.velocity = -firePoint.transform.up * (playerPower * basePower);

        ItemSpawner.PlaySFX("playerShoot");
        canShoot = false;
    }

    float GetPlayerPower()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        float power = Vector2.Distance(firePoint.position, mousePos) / playerPowerRange;
        return Mathf.Clamp(power, MIN_POWER_SCALED, MAX_POWER_SCALED);
    }

    public bool CanAngleChange()
    {
        return aimType == AimType.AllMouse || aimType == AimType.Calculated && !isAngleSet;
    }

    public bool CanPowerChange()
    {
        return aimType == AimType.AllMouse || aimType == AimType.Calculated && !isPowerSet;
    }

    void ResetCalculated()
    {
        isPowerSet = false;
        isAngleSet = false;
        playerPower = 0;
    }

    void Calculated()
    {
        if (!isAngleSet)
        {
            ItemSpawner.SpawnText(aimText.transform.position, "Angle Set!");
            isAngleSet = true;
        }
        else
        {
            if (!isPowerSet)
            {
                playerPower = GetPlayerPower();

                ItemSpawner.SpawnText(aimText.transform.position, "Power Set!");

                isPowerSet = true;
            }
            else
            {
                if (isPowerSet && isAngleSet)
                {
                    EventManager.Invoke(CustomEvent.PlayerShootStart);
                }
            }
        }
    }

    void SetPlayerPower()
    {
        playerPower = GetPlayerPower();
        AimUI.Instance.SetBarPower(playerPower);
    }

    private void Update()
    {
        if (!canShoot) return;

        SetPlayerPower();

        if (Input.GetMouseButtonUp(0))
        {
            if (aimType == AimType.AllMouse)
            {
                EventManager.Invoke(CustomEvent.PlayerShootStart);
            }
            else if (aimType == AimType.Calculated)
            {
                Calculated();
            }
        }

        if (Input.GetMouseButtonDown(1))
        {
            if (aimType == AimType.Calculated)
            {
                ItemSpawner.SpawnText(aimText.transform.position, "Shot Reset!");
                ResetCalculated();
            }
        }
    }
}