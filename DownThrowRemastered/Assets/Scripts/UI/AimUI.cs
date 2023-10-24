using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AimUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI ballsLeftText;
    [Space(10)]
    [SerializeField] Image barPower;
    [SerializeField] Gradient barPowerGradient;

    private void OnEnable()
    {
        EventManager.OnPlayerShoot += EventManager_OnPlayerShoot;
    }

    private void OnDisable()
    {
        EventManager.OnPlayerShoot -= EventManager_OnPlayerShoot;
    }

    private void EventManager_OnPlayerShoot()
    {
        SetBallsLeftText(GameManager.player);
    }

    void SetBallsLeftText(InGamePlayer player)
    {
        ballsLeftText.SetText(player.GetHealth().ToString());
    }

    public void SetBarPower(float power)
    {
        barPower.fillAmount = power;
        barPower.color = barPowerGradient.Evaluate(power);
    }
}