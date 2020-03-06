using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUITime : MonoBehaviour
{
    // init some private vars
    private TextMesh textMesh;
    private float totalTime;
    private float startTime;
    private Player playerScript;

    // Start is called before the first frame update
    void Start()
    {
        // get the textMesh and Player components and save for later
        textMesh = GetComponent<TextMesh>();
        playerScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Player>();

        // init the timer on screen
        startTime = Time.time;
        textMesh.text = "Time: 00:00";
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // check if the player isn't dead
        if (!playerScript.dead)
        {
            // update totalTime
            totalTime = Time.time - startTime;

            // update the timer on screen
            string minutes = Mathf.Floor(totalTime / 60).ToString("00");
            string seconds = (totalTime % 60).ToString("00");
            textMesh.text = "Time: " + minutes + ":" + seconds;
        }
    }
}
