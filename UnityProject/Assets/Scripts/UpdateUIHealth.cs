using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateUIHealth : MonoBehaviour
{
    // init some vars editable in Unity
    public Player player;

    // init some private vars
    private TextMesh textMesh;

    // Start is called before the first frame update
    void Start()
    {
        // grab the textMesh component on startup
        textMesh = GetComponent<TextMesh>();

        // init the HUD's health display
        textMesh.text = "Health: " + player.currentHealth + "/" + player.startingHealth;
    }

    // Update is called once per frame
    void Update()
    {
        // update the HUD's health display
        textMesh.text = "Health: " + player.currentHealth + "/" + player.startingHealth;
    }
}
