using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AimController : MonoBehaviour
{
    [SerializeField] float basePower = 10f;
    [SerializeField] [Range(1f, 10f)] float playerPowerRange = 7.5f;
    [Space(10)]
    [SerializeField] Transform firePoint;

    private bool canShoot = true;

    private bool isAngleSet = false;
    private bool isPowerSet = false;
    private float calculatedPower = 0;

    [SerializeField] AimType aimType;

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
        ResetCalculated();
    }

    private void EventManager_onPlayerShoot()
    {
        Rigidbody2D ball = ItemSpawner.SpawnGame("Ball", firePoint.transform.position).GetComponent<Rigidbody2D>();

        if (aimType == AimType.AllMouse)
        {
            Debug.Log(GetPlayerPower());
            ball.velocity = -firePoint.transform.up * (GetPlayerPower() * basePower);
        }
        else if (aimType == AimType.Calculated)
        {
            Debug.Log(calculatedPower);
            ball.velocity = -firePoint.transform.up * (calculatedPower * basePower);
        }

        ItemSpawner.PlaySFX("playerShoot");
        canShoot = false;
    }


    float GetPlayerPower()
    {
        Vector2 mousePos = cam.ScreenToWorldPoint(Input.mousePosition);
        float power = (firePoint.position.y - mousePos.y) / playerPowerRange;
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
        calculatedPower = 0;
    }

    void Calculated()
    {
        if (!isAngleSet)
        {
            Debug.Log("Angle Set!");
            isAngleSet = true;
        }
        else
        {
            if (!isPowerSet)
            {
                calculatedPower = GetPlayerPower();
                Debug.Log("Power Set @ " + calculatedPower);

                isPowerSet = true;
            }
            else
            {
                if (isPowerSet && isAngleSet)
                {
                    Debug.Log("Fire!");
                    EventManager.Invoke(CustomEvent.PlayerShootStart);
                }
            }
        }
    }

    private void Update()
    {
        if (!canShoot) return;

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
                Debug.Log("Reset!");
                ResetCalculated();
            }
        }
    }
}