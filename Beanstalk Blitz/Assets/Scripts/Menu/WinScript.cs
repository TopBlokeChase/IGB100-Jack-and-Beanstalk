using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using BeanstalkBlitz;
using UnityEngine.SceneManagement;

public class WinScript : MonoBehaviour
{
    private bool canRotate = true;
    private bool canMoveCamera = true;
    public GameObject winScreen;
    public static bool hasWon = false;
    private PlayerCameraBehaviour playerCamera;

    private void Start()
    {
        playerCamera = FindObjectOfType<PlayerCameraBehaviour>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !hasWon)
        {
            hasWon = true;
            Toggle(true);
            Time.timeScale = 0f;
            canRotate = false;

            
            if (playerCamera != null)
            {
                playerCamera.SetAllowCameraRotation(false);
            }
        }
    }

    public void Toggle(bool state)
    {
        winScreen.SetActive(state);

        if (state)
        {
            
            canMoveCamera = false;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
            Time.timeScale = 0f;
        }
        else
        {
            
            canMoveCamera = true;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
            Time.timeScale = 1f;
            ResetCamera();
        }
    }

    public void ResetCamera()
    {
        if (playerCamera != null)
        {
            playerCamera.SetAllowCameraRotation(true);
        }
    }

    public void ResetWinStatus()
    {
        hasWon = false;
        canRotate = true;
        canMoveCamera = true;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        Time.timeScale = 1f;
        winScreen.SetActive(false);
    }

    private void Update()
    {
        if (!canRotate && !canMoveCamera && (Cursor.lockState != CursorLockMode.None || Cursor.visible != true))
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    private void OnDestroy()
    {
        ResetWinStatus(); 
    }
}