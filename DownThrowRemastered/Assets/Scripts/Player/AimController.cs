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
    private float playerPower = 0;

    private const float MIN_POWER = .1f;
    private const float MAX_POWER = 1f;

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
    }

    private void EventManager_onPlayerShoot()
    {
        Rigidbody2D ball = ItemSpawner.SpawnGame("Ball", firePoint.transform.position, ballParent).GetComponent<Rigidbody2D>();
        ball.velocity = -firePoint.transform.up * (playerPower * basePower);

        ItemSpawner.PlaySFX("playerShoot");
        canShoot = false;
    }

    void SetPlayerPower()
    {
        AimUI.Instance.SetBarPower(playerPower);
    }

    private void FixedUpdate()
    {
        if (!canShoot) return;

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
        SetPlayerPower();
    }

    private void Update()
    {
        if (!canShoot) return;

        if (Input.GetKeyDown(KeyCode.Space))
        {
            EventManager.Invoke(CustomEvent.PlayerShootStart);
        }
    }
}