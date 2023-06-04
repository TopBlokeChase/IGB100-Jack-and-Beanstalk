using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MainMenu : MonoBehaviour
{
    public static bool hasWon = false;
    public void PlayGame()
    {
        hasWon = false;
        SceneManager.LoadScene("Main Game");
    }

    public void QuitGame()
    {
        Application .Quit();

    }
    public GameObject mainMenuPanel;
    public GameObject instructionPanel;

    public void ShowInstructions()
    {
        mainMenuPanel.SetActive(false);
        instructionPanel.SetActive(true);
    }

    public void ShowMainMenu()
    {
        mainMenuPanel.SetActive(true);
        instructionPanel.SetActive(false);
    }



}
