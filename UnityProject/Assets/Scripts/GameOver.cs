using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameOver : MonoBehaviour
{
    // function called to load the main menu scene
    public void MainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    // function called to quit the game
    public void Quit()
    {
        Application.Quit();
    }

    // function called to play the game
    public void Play()
    {
        SceneManager.LoadScene("FightScene");
    }
}
