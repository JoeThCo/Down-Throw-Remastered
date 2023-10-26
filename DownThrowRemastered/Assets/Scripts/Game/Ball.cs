using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    [HideInInspector] public int damage;

    public void ChangeDamage(int change)
    {
        damage += change;
    }
}