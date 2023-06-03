using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StemStatTracker : MonoBehaviour
{
    [SerializeField]
    private int health;
    [SerializeField]
    private int maxHealth;

    [SerializeField]
    bool hasMuncher;
    [SerializeField]
    bool hasBonker;
    [SerializeField]
    bool hasWebslinger;

    [SerializeField]
    bool hasBean;

    // Get methods
    public int Health { get { return health; } }
    public int MaxHealth { get { return maxHealth; } }

    public bool HasMuncher { get { return hasMuncher; } }
    public bool HasBonker { get { return hasBonker; } }
    public bool HasWebslinger { get { return hasWebslinger; } }
    public bool HasBean { get { return hasBean; } }

    public void InitialiseVariables(int value)
    {
        maxHealth = value;
        health = value;
        hasMuncher = false;
        hasBonker = false;
        hasWebslinger = false;
    }

    public void ChangeHealth(int value)
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

    public void ToggleMuncher(bool state)
    {
        hasMuncher = state;
    }
    public void ToggleBonker(bool state)
    {
        hasBonker = state;
    }
    // Webslinger not implemented in current version
    /*public void ToggleWebslinger(bool state)
    {
        hasWebslinger = state;
    }*/
    public void ToggleBean(bool state)
    {
        hasBean = state;
    }
}
