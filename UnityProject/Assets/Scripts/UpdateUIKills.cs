using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUIKills : MonoBehaviour
{
    // init some private vars
    private TextMesh textMesh;

    // Start is called before the first frame update
    void Start()
    {
        // grab the TextMesh component
        textMesh = GetComponent<TextMesh>();

        // init the kill counter on screen
        textMesh.text = "Kills: 0";
    }

    void FixedUpdate()
    {
        // get the most recent kill count from the player
        textMesh.text = "Kills: " + GameObject.FindGameObjectWithTag("Player").GetComponent<Player>().enemiesDefeated;
    }
}
