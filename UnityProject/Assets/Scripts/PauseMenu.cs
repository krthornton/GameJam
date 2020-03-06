using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    // static variable to keep up with if the game is paused or not
    public static bool isPaused = false;
    public GameObject pauseMenuUI;
    public AudioSource music;

    // Update is called once per frame
    void Update()
    {
        // check if the user has pressed the escape key
        if (Input.GetKeyDown(KeyCode.Escape))
		{
            if (isPaused)
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
        isPaused = false;
        music.UnPause();
	}

    public void Pause()
	{
        pauseMenuUI.SetActive(true);
        Time.timeScale = 0f;
        isPaused = true;
        music.Pause();
	}

    public void MainMenu()
    {
        // call Resume to fix timescale and paused bool
        Resume();

        // load the MainMenu scene
        SceneManager.LoadScene("MainMenu");
    }

    public void Quit()
    {
        // quit the game
        Application.Quit();
    }
}
