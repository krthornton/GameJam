using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverTime : MonoBehaviour
{
    // init some private vars
    float totalTime;
    string minutes;
    string seconds;

    // Start is called before the first frame update
    void Start()
    {
        // load the player's time
        totalTime = PlayerPrefs.GetFloat("time_alive");

        // format the time properly
        minutes = Mathf.Floor(totalTime / 60).ToString("00");
        seconds = (totalTime % 60).ToString("00");

        // display the player's time on screen
        GetComponent<TextMesh>().text = "Time Alive: " + minutes + ":" + seconds;
    }
}
