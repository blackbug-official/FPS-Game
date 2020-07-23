﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerHealth : MonoBehaviour
{
    [SerializeField] float hitPoints = 100f;

    public void TakeDamage(float Damage)
    {
        hitPoints -= Damage;

        if (hitPoints <= 0)
        {
            print("player dead");
        }
    }
}
