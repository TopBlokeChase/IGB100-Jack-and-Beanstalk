using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    public static bool GameIsPaused = false;

    public GameObject pauseMenuUI;
    private SwingingDone swingingDoneScript;
    private Grappling grapplingScript;

    void Start()
    {
        pauseMenuUI.SetActive(false);
        swingingDoneScript = FindObjectOfType<SwingingDone>();
        grapplingScript = FindObjectOfType<Grappling>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Resume()
    {
        pauseMenuUI.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;

        if (swingingDoneScript != null)
        {
            swingingDoneScript.enabled = true;
        }

        if (grapplingScript != null)
        {
            grapplingScript.enabled = true;
        }

        // Set the mouse visibility and lock state
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    void Pause()
    {
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;

        if (swingingDoneScript != null)
        {
            swingingDoneScript.enabled = false;
        }

        if (grapplingScript != null)
        {
            grapplingScript.enabled = false;
        }

        // Set the mouse visibility and unlock state
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void LoadMenu()
    {
        PauseMenu.ResetPauseStatus();
        SceneManager.LoadScene("Menu Scene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public static void ResetPauseStatus()
    {
        GameIsPaused = false;
        Time.timeScale = 1f;
    }
}
