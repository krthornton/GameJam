using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverDefeated : MonoBehaviour
{
    // init some private vars
    int enemiesDefeated;

    // Start is called before the first frame update
    void Start()
    {
        // load enemiesDefeated
        enemiesDefeated = PlayerPrefs.GetInt("enemies_defeated");

        // display on screen
        GetComponent<TextMesh>().text = "Enemies Defeated: " + enemiesDefeated;
    }
}
