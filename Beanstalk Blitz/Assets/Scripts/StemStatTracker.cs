using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemStatTracker : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;

    public int Health
    {
        get { return health; }
    }

    public int MaxHealth
    {
        get { return maxHealth; }
    }

    public void InitialiseVariables(int value)
    {
        maxHealth = value;
        health = value;
    }

    public void changeHealth(int value)
    {
        health += value;
        if (health > maxHealth)
        {
            health = maxHealth;
        } else if (health < 0)
        {
            health = 0;
        }
    }
}
