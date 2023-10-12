using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AimUI : MonoBehaviour, IUIInit
{
    [SerializeField] TextMeshProUGUI ballsLeftText;
    [Space(10)]
    [SerializeField] Image barPower;
    [SerializeField] Gradient barPowerGradient;

    public static AimUI Instance;

    public void Init()
    {
        Instance = this;
    }

    public void SetBallsLeftText(Player player)
    {
        ballsLeftText.SetText(player.GetHealth().ToString());
    }

    public void SetBarPower(float power)
    {
        barPower.fillAmount = power;
        barPower.color = barPowerGradient.Evaluate(power);
    }
}