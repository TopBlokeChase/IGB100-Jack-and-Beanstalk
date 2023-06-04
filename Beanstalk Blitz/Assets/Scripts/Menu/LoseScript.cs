using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class LoseScript : MonoBehaviour
{
    public GameObject loseScreen;

    public void Toggle(bool state)
    {
        loseScreen.SetActive(false);
    }
}