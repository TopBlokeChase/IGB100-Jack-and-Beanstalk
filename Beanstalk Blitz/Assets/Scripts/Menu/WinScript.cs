using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class WinScript : MonoBehaviour
{
    public GameObject winScreen;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            winScreen.SetActive(true);
            Time.timeScale = 0f;
        }
    }
}