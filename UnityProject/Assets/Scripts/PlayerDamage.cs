using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamage : MonoBehaviour
{
    // init some vars editable in Unity
    public int startingHealth;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // function that is called when this object collides with another object
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // verify the other object is an enemy
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Debug.Log("Collided with enemy!");
        }
    }
}
