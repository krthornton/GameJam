using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    // init some vars editable in Unity
    public int startingHealth;
    [Tooltip("Specifies the amount of damage dealt to enemies when attacking.")]
    public int attackPoints;

    // init some private vars
    private int score = 0;
    private List<GameObject> nearbyEnemies = new List<GameObject>();
    private int currentHealth;
    private Rigidbody2D rb;
    private Animator anim;
    private bool dead = false;

    public void Start()
    {
        // init the player's health
        currentHealth = startingHealth;

        // init rb
        rb = GetComponent<Rigidbody2D>();

        // init anim
        anim = GetComponent<Animator>();
    }

    public void Update()
    {
        // check if the player is dead
        if (!dead && currentHealth <= 0)
        {
            // call Die
            Die();
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // init some vars
        GameObject enemy = collision.gameObject;
        Enemy script = enemy.GetComponent<Enemy>();

        // verify that this is an enemy
        if (enemy.CompareTag("Enemy") && !nearbyEnemies.Contains(enemy))
        {
            // add enemy to nearby list
            nearbyEnemies.Add(enemy);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        // init some vars
        GameObject enemy = collision.gameObject;

        // verfy that this is an enemy
        if (enemy.CompareTag("Enemy") && nearbyEnemies.Contains(enemy))
        {
            // remove enemy from the nearby list
            nearbyEnemies.Remove(enemy);
        }
    }

    // Function called when the player is attacking an enemy
    public void Attack()
    {
        // iterate through all the nearby enemies...
        foreach (GameObject enemy in nearbyEnemies)
        {
            // ...and damage each one
            enemy.GetComponent<Enemy>().Damaged(attackPoints);
        }
    }

    // Function called when an enemy attacks the player
    public void Damaged(int amount, Vector2 direction)
    {
        // subtract from the player's health
        currentHealth -= amount;

        // add force to the player to knock them back
        if (direction.x > 0f)
        {
            rb.AddForce(new Vector2(-200f, 200f));
        }
        else
        {
            rb.AddForce(new Vector2(200f, 200f));
        }

        // play the hurt animation
        anim.Play("Player_Hurt");
    }

    // Function called to update the player's score
    public void UpdateScore(int amount)
    {
        // add amount to current score
        score += amount;
    }

    // Function called when the player is dead
    public void Die()
    {
        // update dead
        dead = true;

        Debug.Log("Player is dead!");
    }
}
