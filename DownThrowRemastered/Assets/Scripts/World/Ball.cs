using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public int damage;

    public void ChangeDamage(int change) 
    {
        damage += change;
    }
}